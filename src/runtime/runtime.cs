using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Python.Runtime
{
   

    /// <summary>
    /// Encapsulates the low-level Python C API. Note that it is
    /// the responsibility of the caller to have acquired the GIL
    /// before calling any of these methods.
    /// </summary>
    public class Runtime
    {

        public static class OS
        {
            public static bool IsPosix { get { return !IsWindows; } }
            public static bool IsLinux
            {
                get
                {
#if NETSTANDARD || NETCOREAPP
                    return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
#else
                return System.Environment.OSVersion.Platform == PlatformID.Unix;
#endif
                }
            }

            public static bool IsOSX
            {
                get
                {
#if NETSTANDARD || NETCOREAPP
                    return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
#else
                return System.Environment.OSVersion.Platform == PlatformID.MacOSX;
#endif
                }
            }
            public static bool IsWindows
            {
                get
                {
#if NETSTANDARD || NETCOREAPP
                    return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#else
                return System.Environment.OSVersion.Platform == PlatformID.Win32NT;
#endif
                }

            }
        }

        public static IInterop Interop;

        static Runtime()
        {
            if (OS.IsPosix)
                Interop = new InteropPosix();
            else
                Interop = new InteropWindows();
        }
        // C# compiler copies constants to the assemblies that references this library.
        // We needs to replace all public constants to static readonly fields to allow 
        // binary substitution of different Python.Runtime.dll builds in a target application.

        public static int UCS => _UCS;

#if UCS4
        internal const int _UCS = 4;

        /// <summary>
        /// EntryPoint to be used in DllImport to map to correct Unicode
        /// methods prior to PEP393. Only used for PY27.
        /// </summary>
        private const string PyUnicodeEntryPoint = "PyUnicodeUCS4_";
#elif UCS2
        internal const int _UCS = 2;

        /// <summary>
        /// EntryPoint to be used in DllImport to map to correct Unicode
        /// methods prior to PEP393. Only used for PY27.
        /// </summary>
        private const string PyUnicodeEntryPoint = "PyUnicodeUCS2_";
#else
#error You must define either UCS2 or UCS4!
#endif

        // C# compiler copies constants to the assemblies that references this library.
        // We needs to replace all public constants to static readonly fields to allow 
        // binary substitution of different Python.Runtime.dll builds in a target application.

        public static string pyversion => _pyversion;
        public static string pyver => _pyver;

#if PYTHON27
        internal const string _pyversion = "2.7";
        internal const string _pyver = "27";
#elif PYTHON33
        internal const string _pyversion = "3.3";
        internal const string _pyver = "33";
#elif PYTHON34
        internal const string _pyversion = "3.4";
        internal const string _pyver = "34";
#elif PYTHON35
        internal const string _pyversion = "3.5";
        internal const string _pyver = "35";
#elif PYTHON36
        internal const string _pyversion = "3.6";
        internal const string _pyver = "36";
#elif PYTHON37 // TODO: Add `interop37.cs` after PY37 is released
        internal const string _pyversion = "3.7";
        internal const string _pyver = "37";
#else
#error You must define one of PYTHON33 to PYTHON37 or PYTHON27
#endif


        // C# compiler copies constants to the assemblies that references this library.
        // We needs to replace all public constants to static readonly fields to allow 
        // binary substitution of different Python.Runtime.dll builds in a target application.


        public static readonly int pyversionnumber = Convert.ToInt32(_pyver);

        // set to true when python is finalizing
        internal static object IsFinalizingLock = new object();
        internal static bool IsFinalizing;

        internal static bool Is32Bit = IntPtr.Size == 4;

        // .NET core: System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        internal static bool IsWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

        internal static bool IsPython2 = pyversionnumber < 30;
        internal static bool IsPython3 = pyversionnumber >= 30;

        /// <summary>
        /// Encoding to use to convert Unicode to/from Managed to Native
        /// </summary>
        internal static readonly Encoding PyEncoding = _UCS == 2 ? Encoding.Unicode : Encoding.UTF32;

        /// <summary>
        /// Initialize the runtime...
        /// </summary>
        internal static void Initialize()
        {
            if (Interop.Py_IsInitialized() == 0)    
            {
                Interop.Py_Initialize();
            }

            if (Interop.PyEval_ThreadsInitialized() == 0)
            {
                Interop.PyEval_InitThreads();
            }

            IntPtr op;
            IntPtr dict;
            if (IsPython3)
            {
                op = Interop.PyImport_ImportModule("builtins");
                dict = Interop.PyObject_GetAttrString(op, "__dict__");
            }
            else // Python2
            {
                dict = Interop.PyImport_GetModuleDict();
                op = Interop.PyDict_GetItemString(dict, "__builtin__");
            }
            PyNotImplemented = Interop.PyObject_GetAttrString(op, "NotImplemented");
            PyBaseObjectType = Interop.PyObject_GetAttrString(op, "object");

            PyModuleType = PyObject_Type(op);
            PyNone = Interop.PyObject_GetAttrString(op, "None");
            PyTrue = Interop.PyObject_GetAttrString(op, "True");
            PyFalse = Interop.PyObject_GetAttrString(op, "False");

            PyBoolType = PyObject_Type(PyTrue);
            PyNoneType = PyObject_Type(PyNone);
            PyTypeType = PyObject_Type(PyNoneType);

            op = Interop.PyObject_GetAttrString(dict, "keys");
            PyMethodType = PyObject_Type(op);
            XDecref(op);

            // For some arcane reason, builtins.__dict__.__setitem__ is *not*
            // a wrapper_descriptor, even though dict.__setitem__ is.
            //
            // object.__init__ seems safe, though.
            op = Interop.PyObject_GetAttrString(PyBaseObjectType, "__init__");
            PyWrapperDescriptorType = PyObject_Type(op);
            XDecref(op);

#if PYTHON3
            XDecref(dict);
#endif

            op = PyString_FromString("string");
            PyStringType = PyObject_Type(op);
            XDecref(op);

            op = PyUnicode_FromString("unicode");
            PyUnicodeType = PyObject_Type(op);
            XDecref(op);

#if PYTHON3
            op = Interop.PyBytes_FromString("bytes");
            PyBytesType = PyObject_Type(op);
            XDecref(op);
#endif

            op = Interop.PyTuple_New(0);
            PyTupleType = PyObject_Type(op);
            XDecref(op);

            op = Interop.PyList_New(0);
            PyListType = PyObject_Type(op);
            XDecref(op);

            op = Interop.PyDict_New();
            PyDictType = PyObject_Type(op);
            XDecref(op);

            op = PyInt_FromInt32(0);
            PyIntType = PyObject_Type(op);
            XDecref(op);

            op = Interop.PyLong_FromLong(0);
            PyLongType = PyObject_Type(op);
            XDecref(op);

            op = Interop.PyFloat_FromDouble(0);
            PyFloatType = PyObject_Type(op);
            XDecref(op);

#if PYTHON3
            PyClassType = IntPtr.Zero;
            PyInstanceType = IntPtr.Zero;
#elif PYTHON2
            IntPtr s = Interop.PyString_FromString("_temp");
            IntPtr d = Interop.PyDict_New();

            IntPtr c = Interop.PyClass_New(IntPtr.Zero, d, s);
            PyClassType = Interop.PyObject_Type(c);

            IntPtr i = Interop.PyInstance_New(c, IntPtr.Zero, IntPtr.Zero);
            PyInstanceType = Interop.PyObject_Type(i);

            XDecref(s);
            XDecref(i);
            XDecref(c);
            XDecref(d);
#endif

            Error = new IntPtr(-1);

            IntPtr dllLocal = IntPtr.Zero;

            if (Interop.GetDllName() != "__Internal")
            {
                if (OS.IsLinux)
                    dllLocal = NativeMethods_Linux.LoadLibrary(Interop.GetDllName());
                else if (OS.IsOSX)
                    dllLocal = NativeMethods_OSX.LoadLibrary(Interop.GetDllName());
                else if (OS.IsWindows)
                    dllLocal = NativeMethods_Windows.LoadLibrary(Interop.GetDllName());
            }

            if (OS.IsLinux)
                _PyObject_NextNotImplemented = NativeMethods_Linux.GetProcAddress(dllLocal, "_PyObject_NextNotImplemented");
            else if (OS.IsOSX)
                _PyObject_NextNotImplemented = NativeMethods_OSX.GetProcAddress(dllLocal, "_PyObject_NextNotImplemented");
            else if (OS.IsWindows)
                _PyObject_NextNotImplemented = NativeMethods_Windows.GetProcAddress(dllLocal, "_PyObject_NextNotImplemented");

            

            if (OS.IsWindows && dllLocal != IntPtr.Zero)
            {
                NativeMethods_Windows.FreeLibrary(dllLocal);
            }

            // Initialize modules that depend on the runtime class.
            AssemblyManager.Initialize();
            PyCLRMetaType = MetaType.Initialize();
            Exceptions.Initialize();
            ImportHook.Initialize();

            // Need to add the runtime directory to sys.path so that we
            // can find built-in assemblies like System.Data, et. al.
            string rtdir = RuntimeEnvironment.GetRuntimeDirectory();
            IntPtr path = Interop.PySys_GetObject("path");
            IntPtr item = PyString_FromString(rtdir);
            Interop.PyList_Append(path, item);
            XDecref(item);
            AssemblyManager.UpdatePath();
        }
        internal static IntPtr PyBytes_AS_STRING(IntPtr ob)
        {
            return ob + BytesOffset.ob_sval;
        }

        internal static void Shutdown()
        {
            AssemblyManager.Shutdown();
            Exceptions.Shutdown();
            ImportHook.Shutdown();
            Interop.Py_Finalize();
        }

        // called *without* the GIL acquired by clr._AtExit
        internal static int AtExit()
        {
            lock (IsFinalizingLock)
            {
                IsFinalizing = true;
            }
            return 0;
        }

        internal static IntPtr Py_single_input = (IntPtr)256;
        internal static IntPtr Py_file_input = (IntPtr)257;
        internal static IntPtr Py_eval_input = (IntPtr)258;

        internal static IntPtr PyBaseObjectType;
        internal static IntPtr PyModuleType;
        internal static IntPtr PyClassType;
        internal static IntPtr PyInstanceType;
        internal static IntPtr PyCLRMetaType;
        internal static IntPtr PyMethodType;
        internal static IntPtr PyWrapperDescriptorType;

        internal static IntPtr PyUnicodeType;
        internal static IntPtr PyStringType;
        internal static IntPtr PyTupleType;
        internal static IntPtr PyListType;
        internal static IntPtr PyDictType;
        internal static IntPtr PyIntType;
        internal static IntPtr PyLongType;
        internal static IntPtr PyFloatType;
        internal static IntPtr PyBoolType;
        internal static IntPtr PyNoneType;
        internal static IntPtr PyTypeType;

#if PYTHON3
        internal static IntPtr PyBytesType;
#endif
        internal static IntPtr _PyObject_NextNotImplemented;

        internal static IntPtr PyNotImplemented;
        internal const int Py_LT = 0;
        internal const int Py_LE = 1;
        internal const int Py_EQ = 2;
        internal const int Py_NE = 3;
        internal const int Py_GT = 4;
        internal const int Py_GE = 5;

        internal static IntPtr PyTrue;
        internal static IntPtr PyFalse;
        internal static IntPtr PyNone;
        internal static IntPtr Error;

        /// <summary>
        /// Check if any Python Exceptions occurred.
        /// If any exist throw new PythonException.
        /// </summary>
        /// <remarks>
        /// Can be used instead of `obj == IntPtr.Zero` for example.
        /// </remarks>
        internal static void CheckExceptionOccurred()
        {
            if (Interop.PyErr_Occurred() != 0)
            {
                throw new PythonException();
            }
        }

        internal static IntPtr ExtendTuple(IntPtr t, params IntPtr[] args)
        {
            int size = Interop.PyTuple_Size(t);
            int add = args.Length;
            IntPtr item;

            IntPtr items = Interop.PyTuple_New(size + add);
            for (var i = 0; i < size; i++)
            {
                item = Interop.PyTuple_GetItem(t, i);
                XIncref(item);
                Interop.PyTuple_SetItem(items, i, item);
            }

            for (var n = 0; n < add; n++)
            {
                item = args[n];
                XIncref(item);
                Interop.PyTuple_SetItem(items, size + n, item);
            }

            return items;
        }

        internal static Type[] PythonArgsToTypeArray(IntPtr arg)
        {
            return PythonArgsToTypeArray(arg, false);
        }

        internal static Type[] PythonArgsToTypeArray(IntPtr arg, bool mangleObjects)
        {
            // Given a PyObject * that is either a single type object or a
            // tuple of (managed or unmanaged) type objects, return a Type[]
            // containing the CLR Type objects that map to those types.
            IntPtr args = arg;
            var free = false;

            if (!PyTuple_Check(arg))
            {
                args = Interop.PyTuple_New(1);
                XIncref(arg);
                Interop.PyTuple_SetItem(args, 0, arg);
                free = true;
            }

            int n = Interop.PyTuple_Size(args);
            var types = new Type[n];
            Type t = null;

            for (var i = 0; i < n; i++)
            {
                IntPtr op = Interop.PyTuple_GetItem(args, i);
                if (mangleObjects && (!PyType_Check(op)))
                {
                    op = PyObject_TYPE(op);
                }
                ManagedType mt = ManagedType.GetManagedObject(op);

                if (mt is ClassBase)
                {
                    t = ((ClassBase)mt).type;
                }
                else if (mt is CLRObject)
                {
                    object inst = ((CLRObject)mt).inst;
                    if (inst is Type)
                    {
                        t = inst as Type;
                    }
                }
                else
                {
                    t = Converter.GetTypeByAlias(op);
                }

                if (t == null)
                {
                    types = null;
                    break;
                }
                types[i] = t;
            }
            if (free)
            {
                XDecref(args);
            }
            return types;
        }

        /// <summary>
        /// Managed exports of the Python C API. Where appropriate, we do
        /// some optimization to avoid managed &lt;--&gt; unmanaged transitions
        /// (mostly for heavily used methods).
        /// </summary>
        internal static unsafe void XIncref(IntPtr op)
        {
#if PYTHON_WITH_PYDEBUG || NETSTANDARD || NETCOREAPP
            Interop.Py_IncRef(op);
            return;
#else
            var p = (void*)op;
            if ((void*)0 != p)
            {
                if (Is32Bit)
                {
                    (*(int*)p)++;
                }
                else
                {
                    (*(long*)p)++;
                }
            }
#endif
        }

        internal static unsafe void XDecref(IntPtr op)
        {
#if PYTHON_WITH_PYDEBUG || NETSTANDARD || NETCOREAPP
            Interop.Py_DecRef(op);
            return;
#else
            var p = (void*)op;
            if ((void*)0 != p)
            {
                if (Is32Bit)
                {
                    --(*(int*)p);
                }
                else
                {
                    --(*(long*)p);
                }
                if ((*(int*)p) == 0)
                {
                    // PyObject_HEAD: struct _typeobject *ob_type
                    void* t = Is32Bit
                        ? (void*)(*((uint*)p + 1))
                        : (void*)(*((ulong*)p + 1));
                    // PyTypeObject: destructor tp_dealloc
                    void* f = Is32Bit
                        ? (void*)(*((uint*)t + 6))
                        : (void*)(*((ulong*)t + 6));
                    if ((void*)0 == f)
                    {
                        return;
                    }
                    NativeCall.Impl.Void_Call_1(new IntPtr(f), op);
                }
            }
#endif
        }

        internal static unsafe long Refcount(IntPtr op)
        {
            var p = (void*)op;
            if ((void*)0 == p)
            {
                return 0;
            }
            return Is32Bit ? (*(int*)p) : (*(long*)p);
        }

    


        //====================================================================
        // Python abstract object API
        //====================================================================

        /// <summary>
        /// A macro-like method to get the type of a Python object. This is
        /// designed to be lean and mean in IL &amp; avoid managed &lt;-&gt; unmanaged
        /// transitions. Note that this does not incref the type object.
        /// </summary>
        internal static unsafe IntPtr PyObject_TYPE(IntPtr op)
        {
            var p = (void*)op;
            if ((void*)0 == p)
            {
                return IntPtr.Zero;
            }
#if PYTHON_WITH_PYDEBUG
            var n = 3;
#else
            var n = 1;
#endif
            return Is32Bit
                ? new IntPtr((void*)(*((uint*)p + n)))
                : new IntPtr((void*)(*((ulong*)p + n)));
        }

        /// <summary>
        /// Managed version of the standard Python C API PyObject_Type call.
        /// This version avoids a managed  &lt;-&gt; unmanaged transition.
        /// This one does incref the returned type object.
        /// </summary>
        internal static IntPtr PyObject_Type(IntPtr op)
        {
            IntPtr tp = PyObject_TYPE(op);
            XIncref(tp);
            return tp;
        }

        internal static string PyObject_GetTypeName(IntPtr op)
        {
            IntPtr pyType = Marshal.ReadIntPtr(op, ObjectOffset.ob_type);
            IntPtr ppName = Marshal.ReadIntPtr(pyType, TypeOffset.tp_name);
            return Marshal.PtrToStringAnsi(ppName);
        }

        /// <summary>
        /// Test whether the Python object is an iterable.
        /// </summary>
        internal static bool PyObject_IsIterable(IntPtr pointer)
        {
            var ob_type = Marshal.ReadIntPtr(pointer, ObjectOffset.ob_type);
#if PYTHON2
            long tp_flags = Util.ReadCLong(ob_type, TypeOffset.tp_flags);
            if ((tp_flags & TypeFlags.HaveIter) == 0)
                return false;
#endif
            IntPtr tp_iter = Marshal.ReadIntPtr(ob_type, TypeOffset.tp_iter);
            return tp_iter != IntPtr.Zero;
        }

       
        internal static int PyObject_Compare(IntPtr value1, IntPtr value2)
        {
            int res;
            res = Interop.PyObject_RichCompareBool(value1, value2, Py_LT);
            if (-1 == res)
                return -1;
            else if (1 == res)
                return -1;

            res = Interop.PyObject_RichCompareBool(value1, value2, Py_EQ);
            if (-1 == res)
                return -1;
            else if (1 == res)
                return 0;

            res = Interop.PyObject_RichCompareBool(value1, value2, Py_GT);
            if (-1 == res)
                return -1;
            else if (1 == res)
                return 1;

            Exceptions.SetError(Exceptions.SystemError, "Error comparing objects");
            return -1;
        }


        internal static bool PyInt_Check(IntPtr ob)
        {
            return PyObject_TypeCheck(ob, PyIntType);
        }

        internal static bool PyBool_Check(IntPtr ob)
        {
            return PyObject_TypeCheck(ob, PyBoolType);
        }

        internal static IntPtr PyInt_FromInt32(int value)
        {
            var v = new IntPtr(value);
            return Interop.PyInt_FromLong(v);
        }

        internal static IntPtr PyInt_FromInt64(long value)
        {
            var v = new IntPtr(value);
            return Interop.PyInt_FromLong(v);
        }

        internal static bool PyLong_Check(IntPtr ob)
        {
            return PyObject_TYPE(ob) == PyLongType;
        }

        internal static bool PyFloat_Check(IntPtr ob)
        {
            return PyObject_TYPE(ob) == PyFloatType;
        }


        //====================================================================
        // Python string API
        //====================================================================

        internal static bool IsStringType(IntPtr op)
        {
            IntPtr t = PyObject_TYPE(op);
            return (t == PyStringType) || (t == PyUnicodeType);
        }

        internal static bool PyString_Check(IntPtr ob)
        {
            return PyObject_TYPE(ob) == PyStringType;
        }

        internal static IntPtr PyString_FromString(string value)
        {
            return Interop.PyString_FromStringAndSize(value, value.Length);
        }

        internal static bool PyUnicode_Check(IntPtr ob)
        {
            return PyObject_TYPE(ob) == PyUnicodeType;
        }

        internal static IntPtr PyUnicode_FromUnicode(string s, int size)
        {
            return Interop.PyUnicode_FromKindAndData(_UCS, s, size);
        }

        internal static IntPtr PyUnicode_FromString(string s)
        {
            return PyUnicode_FromUnicode(s, s.Length);
        }

        /// <summary>
        /// Function to access the internal PyUnicode/PyString object and
        /// convert it to a managed string with the correct encoding.
        /// </summary>
        /// <remarks>
        /// We can't easily do this through through the CustomMarshaler's on
        /// the returns because will have access to the IntPtr but not size.
        /// <para />
        /// For PyUnicodeType, we can't convert with Marshal.PtrToStringUni
        /// since it only works for UCS2.
        /// </remarks>
        /// <param name="op">PyStringType or PyUnicodeType object to convert</param>
        /// <returns>Managed String</returns>
        internal static string GetManagedString(IntPtr op)
        {
            IntPtr type = PyObject_TYPE(op);

#if PYTHON2 // Python 3 strings are all Unicode
            if (type == PyStringType)
            {
                return Marshal.PtrToStringAnsi(PyString_AsString(op), PyString_Size(op));
            }
#endif

            if (type == PyUnicodeType)
            {
                IntPtr p = Interop.PyUnicode_AsUnicode(op);
                int length = Interop.PyUnicode_GetSize(op);

                int size = length * _UCS;
                var buffer = new byte[size];
                Marshal.Copy(p, buffer, 0, size);
                return PyEncoding.GetString(buffer, 0, size);
            }

            return null;
        }


        //====================================================================
        // Python dictionary API
        //====================================================================

        internal static bool PyDict_Check(IntPtr ob)
        {
            return PyObject_TYPE(ob) == PyDictType;
        }



        //====================================================================
        // Python list API
        //====================================================================

        internal static bool PyList_Check(IntPtr ob)
        {
            return PyObject_TYPE(ob) == PyListType;
        }
        //====================================================================
        // Python tuple API
        //====================================================================

        internal static bool PyTuple_Check(IntPtr ob)
        {
            return PyObject_TYPE(ob) == PyTupleType;
        }

        //====================================================================
        // Python iterator API
        //====================================================================

        internal static bool PyIter_Check(IntPtr pointer)
        {
            var ob_type = Marshal.ReadIntPtr(pointer, ObjectOffset.ob_type);
#if PYTHON2
            long tp_flags = Util.ReadCLong(ob_type, TypeOffset.tp_flags);
            if ((tp_flags & TypeFlags.HaveIter) == 0)
                return false;
#endif
            IntPtr tp_iternext = Marshal.ReadIntPtr(ob_type, TypeOffset.tp_iternext);
            return tp_iternext != IntPtr.Zero && tp_iternext != _PyObject_NextNotImplemented;
        }


        internal static bool PyType_Check(IntPtr ob)
        {
            return PyObject_TypeCheck(ob, PyTypeType);
        }

        internal static bool PyObject_TypeCheck(IntPtr ob, IntPtr tp)
        {
            IntPtr t = PyObject_TYPE(ob);
            return (t == tp) || Interop.PyType_IsSubtype(t, tp);
        }
    }
}
