using Python.Runtime;
using System;

namespace Python.Runtime
{
    internal class InteropWindows : IInterop
    {
        public string GetDllName() {
            return Runtime_Windows._PythonDll;
        }
        public void Py_IncRef(IntPtr ob) { Runtime_Windows.Py_IncRef(ob); }


        public void Py_DecRef(IntPtr ob){  Runtime_Windows.Py_DecRef(ob); }


        public void Py_Initialize(){  Runtime_Windows.Py_Initialize(); }


        public int Py_IsInitialized(){ return Runtime_Windows.Py_IsInitialized(); }


        public void Py_Finalize(){  Runtime_Windows.Py_Finalize(); }


        public IntPtr Py_NewInterpreter(){ return Runtime_Windows.Py_NewInterpreter(); }


        public void Py_EndInterpreter(IntPtr threadState){  Runtime_Windows.Py_EndInterpreter(threadState); }


        public IntPtr PyThreadState_New(IntPtr istate){ return Runtime_Windows.PyThreadState_New(istate); }


        public IntPtr PyThreadState_Get(){ return Runtime_Windows.PyThreadState_Get(); }


        public IntPtr PyThread_get_key_value(IntPtr key){ return Runtime_Windows.PyThread_get_key_value(key); }


        public int PyThread_get_thread_ident(){ return Runtime_Windows.PyThread_get_thread_ident(); }


        public int PyThread_set_key_value(IntPtr key, IntPtr value){ return Runtime_Windows.PyThread_set_key_value(key, value); }


        public IntPtr PyThreadState_Swap(IntPtr key){ return Runtime_Windows.PyThreadState_Swap(key); }


        public IntPtr PyGILState_Ensure(){ return Runtime_Windows.PyGILState_Ensure(); }


        public void PyGILState_Release(IntPtr gs){ Runtime_Windows.PyGILState_Release(gs); }

        public IntPtr PyGILState_GetThisThreadState(){ return Runtime_Windows.PyGILState_GetThisThreadState(); }

#if PYTHON3
        public int Py_Main(
            int argc,
            string[] argv
        ){ return Runtime_Windows.Py_Main(argc, argv); }
#elif PYTHON2
        public  int Py_Main(int argc, string[] argv){ return Runtime_Windows.Py_Main(argc, argv); }
#endif


        public void PyEval_InitThreads(){ Runtime_Windows.PyEval_InitThreads(); }


        public int PyEval_ThreadsInitialized(){ return Runtime_Windows.PyEval_ThreadsInitialized(); }




        public void PyEval_ReleaseLock(){  Runtime_Windows.PyEval_ReleaseLock(); }


        public void PyEval_AcquireThread(IntPtr tstate){  Runtime_Windows.PyEval_AcquireThread(tstate); }


        public void PyEval_ReleaseThread(IntPtr tstate){  Runtime_Windows.PyEval_ReleaseThread(tstate); }


        public IntPtr PyEval_SaveThread(){ return Runtime_Windows.PyEval_SaveThread(); }


        public void PyEval_RestoreThread(IntPtr tstate){  Runtime_Windows.PyEval_RestoreThread(tstate); }


        public IntPtr PyEval_GetBuiltins(){ return Runtime_Windows.PyEval_GetBuiltins(); }


        public IntPtr PyEval_GetGlobals(){ return Runtime_Windows.PyEval_GetGlobals(); }


        public IntPtr PyEval_GetLocals(){ return Runtime_Windows.PyEval_GetLocals(); }


        public IntPtr Py_GetProgramName(){ return Runtime_Windows.Py_GetProgramName(); }


        public void Py_SetProgramName(IntPtr name){  Runtime_Windows.Py_SetProgramName(name); }


        public IntPtr Py_GetPythonHome(){ return Runtime_Windows.Py_GetPythonHome(); }


        public void Py_SetPythonHome(IntPtr home){  Runtime_Windows.Py_SetPythonHome(home); }


        public IntPtr Py_GetPath(){ return Runtime_Windows.Py_GetPath(); }


        public void Py_SetPath(IntPtr home){  Runtime_Windows.Py_SetPath(home); }


        public IntPtr Py_GetVersion(){ return Runtime_Windows.Py_GetVersion(); }


        public IntPtr Py_GetPlatform(){ return Runtime_Windows.Py_GetPlatform(); }


        public IntPtr Py_GetCopyright(){ return Runtime_Windows.Py_GetCopyright(); }


        public IntPtr Py_GetCompiler(){ return Runtime_Windows.Py_GetCompiler(); }


        public IntPtr Py_GetBuildInfo(){ return Runtime_Windows.Py_GetBuildInfo(); }


        public int PyRun_SimpleString(string code){ return Runtime_Windows.PyRun_SimpleString(code); }


        public IntPtr PyRun_String(string code, IntPtr st, IntPtr globals, IntPtr locals){ return Runtime_Windows.PyRun_String(code, st, globals, locals); }


        public IntPtr PyEval_EvalCode(IntPtr co, IntPtr globals, IntPtr locals){ return Runtime_Windows.PyEval_EvalCode(co, globals, locals); }


        public IntPtr Py_CompileString(string code, string file, IntPtr tok){ return Runtime_Windows.Py_CompileString(code, file, tok); }


        public IntPtr PyImport_ExecCodeModule(string name, IntPtr code){ return Runtime_Windows.PyImport_ExecCodeModule(name, code); }


        public IntPtr PyCFunction_NewEx(IntPtr ml, IntPtr self, IntPtr mod){ return Runtime_Windows.PyCFunction_NewEx(ml, self, mod); }


        public IntPtr PyCFunction_Call(IntPtr func, IntPtr args, IntPtr kw){ return Runtime_Windows.PyCFunction_Call(func, args, kw); }


        public IntPtr PyClass_New(IntPtr bases, IntPtr dict, IntPtr name){ return Runtime_Windows.PyClass_New(bases, dict, name); }


        public IntPtr PyInstance_New(IntPtr cls, IntPtr args, IntPtr kw){ return Runtime_Windows.PyInstance_New(cls, args, kw); }


        public IntPtr PyInstance_NewRaw(IntPtr cls, IntPtr dict){ return Runtime_Windows.PyInstance_NewRaw(cls, dict); }


        public IntPtr PyMethod_New(IntPtr func, IntPtr self, IntPtr cls){ return Runtime_Windows.PyMethod_New(func, self, cls); }


        public int PyObject_HasAttrString(IntPtr pointer, string name){ return Runtime_Windows.PyObject_HasAttrString(pointer, name); }


        public IntPtr PyObject_GetAttrString(IntPtr pointer, string name){ return Runtime_Windows.PyObject_GetAttrString(pointer, name); }


        public int PyObject_SetAttrString(IntPtr pointer, string name, IntPtr value){ return Runtime_Windows.PyObject_SetAttrString(pointer, name, value); }


        public int PyObject_HasAttr(IntPtr pointer, IntPtr name){ return Runtime_Windows.PyObject_HasAttr(pointer, name); }


        public IntPtr PyObject_GetAttr(IntPtr pointer, IntPtr name){ return Runtime_Windows.PyObject_GetAttr(pointer, name); }


        public int PyObject_SetAttr(IntPtr pointer, IntPtr name, IntPtr value){ return Runtime_Windows.PyObject_SetAttr(pointer, name, value); }


        public IntPtr PyObject_GetItem(IntPtr pointer, IntPtr key){ return Runtime_Windows.PyObject_GetItem(pointer, key); }


        public int PyObject_SetItem(IntPtr pointer, IntPtr key, IntPtr value){ return Runtime_Windows.PyObject_SetItem(pointer,  key, value); }


        public int PyObject_DelItem(IntPtr pointer, IntPtr key){ return Runtime_Windows.PyObject_DelItem(pointer, key); }


        public IntPtr PyObject_GetIter(IntPtr op){ return Runtime_Windows.PyObject_GetIter(op); }


        public IntPtr PyObject_Call(IntPtr pointer, IntPtr args, IntPtr kw){ return Runtime_Windows.PyObject_Call(pointer, args, kw); }


        public IntPtr PyObject_CallObject(IntPtr pointer, IntPtr args){ return Runtime_Windows.PyObject_CallObject(pointer, args); }

#if PYTHON3

        public int PyObject_RichCompareBool(IntPtr value1, IntPtr value2, int opid){ return Runtime_Windows.PyObject_RichCompareBool(value1, value2, opid); }

#elif PYTHON2
        
        public  int PyObject_Compare(IntPtr value1, IntPtr value2){ return Runtime_Windows.PyObject_Compare(value1, value2); }
#endif


        public int PyObject_IsInstance(IntPtr ob, IntPtr type){ return Runtime_Windows.PyObject_IsInstance(ob, type); }


        public int PyObject_IsSubclass(IntPtr ob, IntPtr type){ return Runtime_Windows.PyObject_IsSubclass(ob, type); }


        public int PyCallable_Check(IntPtr pointer){ return Runtime_Windows.PyCallable_Check(pointer); }


        public int PyObject_IsTrue(IntPtr pointer){ return Runtime_Windows.PyObject_IsTrue(pointer); }


        public int PyObject_Not(IntPtr pointer){ return Runtime_Windows.PyObject_Not(pointer); }


        public int PyObject_Size(IntPtr pointer){ return Runtime_Windows.PyObject_Size(pointer); }


        public IntPtr PyObject_Hash(IntPtr op){ return Runtime_Windows.PyObject_Hash(op); }


        public IntPtr PyObject_Repr(IntPtr pointer){ return Runtime_Windows.PyObject_Repr(pointer); }


        public IntPtr PyObject_Str(IntPtr pointer){ return Runtime_Windows.PyObject_Str(pointer); }

#if PYTHON3
        public IntPtr PyObject_Unicode(IntPtr pointer){ return Runtime_Windows.PyObject_Unicode(pointer); }
#elif PYTHON2
        
        public  IntPtr PyObject_Unicode(IntPtr pointer){ return Runtime_Windows.PyObject_Unicode(opointerb); }
#endif


        public IntPtr PyObject_Dir(IntPtr pointer){ return Runtime_Windows.PyObject_Dir(pointer); }


        //====================================================================
        // Python number API
        //====================================================================

#if PYTHON3
        public IntPtr PyNumber_Int(IntPtr ob){ return Runtime_Windows.PyNumber_Int(ob); }
#elif PYTHON2
        
        public  IntPtr PyNumber_Int(IntPtr ob){ return Runtime_Windows.PyNumber_Int(ob); }
#endif


        public IntPtr PyNumber_Long(IntPtr ob){ return Runtime_Windows.PyNumber_Long(ob); }


        public IntPtr PyNumber_Float(IntPtr ob){ return Runtime_Windows.PyNumber_Float(ob); }


        public bool PyNumber_Check(IntPtr ob){ return Runtime_Windows.PyNumber_Check(ob); }


#if PYTHON3
        public IntPtr PyInt_FromLong(IntPtr value){ return Runtime_Windows.PyInt_FromLong(value); }

        public int PyInt_AsLong(IntPtr value){ return Runtime_Windows.PyInt_AsLong(value); }

        public IntPtr PyInt_FromString(string value, IntPtr end, int radix){ return Runtime_Windows.PyInt_FromString(value, end, radix); }

        public int PyInt_GetMax(){ return Runtime_Windows.PyInt_GetMax(); }
#elif PYTHON2
        
        public  IntPtr PyInt_FromLong(IntPtr value){ return Runtime_Windows.PyInt_FromLong(value); }

        
        public  int PyInt_AsLong(IntPtr value){ return Runtime_Windows.PyInt_AsLong(value); }

        
        public  IntPtr PyInt_FromString(string value, IntPtr end, int radix){ return Runtime_Windows.PyInt_FromString(value, end, radix); }

        
        public  int PyInt_GetMax(){ return Runtime_Windows.PyInt_GetMax(); }
#endif


        public IntPtr PyLong_FromLong(long value){ return Runtime_Windows.PyLong_FromLong(value); }


        public IntPtr PyLong_FromUnsignedLong(uint value){ return Runtime_Windows.PyLong_FromUnsignedLong(value); }


        public IntPtr PyLong_FromDouble(double value){ return Runtime_Windows.PyLong_FromDouble(value); }


        public IntPtr PyLong_FromLongLong(long value){ return Runtime_Windows.PyLong_FromLongLong(value); }


        public IntPtr PyLong_FromUnsignedLongLong(ulong value){ return Runtime_Windows.PyLong_FromUnsignedLongLong(value); }


        public IntPtr PyLong_FromString(string value, IntPtr end, int radix){ return Runtime_Windows.PyLong_FromString(value, end, radix); }


        public int PyLong_AsLong(IntPtr value){ return Runtime_Windows.PyLong_AsLong(value); }


        public uint PyLong_AsUnsignedLong(IntPtr value){ return Runtime_Windows.PyLong_AsUnsignedLong(value); }


        public long PyLong_AsLongLong(IntPtr value){ return Runtime_Windows.PyLong_AsLongLong(value); }


        public ulong PyLong_AsUnsignedLongLong(IntPtr value){ return Runtime_Windows.PyLong_AsUnsignedLongLong(value); }


        public IntPtr PyFloat_FromDouble(double value){ return Runtime_Windows.PyFloat_FromDouble(value); }


        public IntPtr PyFloat_FromString(IntPtr value, IntPtr junk){ return Runtime_Windows.PyFloat_FromString(value, junk); }


        public double PyFloat_AsDouble(IntPtr ob){ return Runtime_Windows.PyFloat_AsDouble(ob); }


        public IntPtr PyNumber_Add(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_Add(o1, o2); }


        public IntPtr PyNumber_Subtract(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_Subtract(o1, o2); }


        public IntPtr PyNumber_Multiply(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_Multiply(o1, o2); }


        public IntPtr PyNumber_Divide(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_Divide(o1, o2); }


        public IntPtr PyNumber_And(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_And(o1, o2); }


        public IntPtr PyNumber_Xor(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_Xor(o1, o2); }


        public IntPtr PyNumber_Or(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_Or(o1, o2); }


        public IntPtr PyNumber_Lshift(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_Lshift(o1, o2); }


        public IntPtr PyNumber_Rshift(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_Rshift(o1, o2); }


        public IntPtr PyNumber_Power(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_Power(o1, o2); }


        public IntPtr PyNumber_Remainder(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_Remainder(o1, o2); }


        public IntPtr PyNumber_InPlaceAdd(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlaceAdd(o1, o2); }


        public IntPtr PyNumber_InPlaceSubtract(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlaceSubtract(o1, o2); }


        public IntPtr PyNumber_InPlaceMultiply(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlaceMultiply(o1, o2); }


        public IntPtr PyNumber_InPlaceDivide(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlaceDivide(o1, o2); }


        public IntPtr PyNumber_InPlaceAnd(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlaceAnd(o1, o2); }


        public IntPtr PyNumber_InPlaceXor(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlaceXor(o1, o2); }


        public IntPtr PyNumber_InPlaceOr(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlaceOr(o1, o2); }


        public IntPtr PyNumber_InPlaceLshift(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlaceLshift(o1, o2); }


        public IntPtr PyNumber_InPlaceRshift(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlaceRshift(o1, o2); }


        public IntPtr PyNumber_InPlacePower(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlacePower(o1, o2); }


        public IntPtr PyNumber_InPlaceRemainder(IntPtr o1, IntPtr o2){ return Runtime_Windows.PyNumber_InPlaceRemainder(o1, o2); }


        public IntPtr PyNumber_Negative(IntPtr o1){ return Runtime_Windows.PyNumber_Negative(o1); }


        public IntPtr PyNumber_Positive(IntPtr o1){ return Runtime_Windows.PyNumber_Positive(o1); }


        public IntPtr PyNumber_Invert(IntPtr o1){ return Runtime_Windows.PyNumber_Invert(o1); }


        public bool PySequence_Check(IntPtr pointer){ return Runtime_Windows.PySequence_Check(pointer); }


        public IntPtr PySequence_GetItem(IntPtr pointer, int index){ return Runtime_Windows.PySequence_GetItem(pointer, index); }


        public int PySequence_SetItem(IntPtr pointer, int index, IntPtr value){ return Runtime_Windows.PySequence_SetItem(pointer, index, value); }


        public int PySequence_DelItem(IntPtr pointer, int index){ return Runtime_Windows.PySequence_DelItem(pointer, index); }


        public IntPtr PySequence_GetSlice(IntPtr pointer, int i1, int i2){ return Runtime_Windows.PySequence_GetSlice(pointer, i1, i2); }


        public int PySequence_SetSlice(IntPtr pointer, int i1, int i2, IntPtr v){ return Runtime_Windows.PySequence_SetSlice(pointer, i1, i2, v); }


        public int PySequence_DelSlice(IntPtr pointer, int i1, int i2){ return Runtime_Windows.PySequence_DelSlice(pointer, i1, i2); }


        public int PySequence_Size(IntPtr pointer){ return Runtime_Windows.PySequence_Size(pointer); }


        public int PySequence_Contains(IntPtr pointer, IntPtr item){ return Runtime_Windows.PySequence_Contains(pointer, item); }


        public IntPtr PySequence_Concat(IntPtr pointer, IntPtr other){ return Runtime_Windows.PySequence_Concat(pointer, other); }


        public IntPtr PySequence_Repeat(IntPtr pointer, int count){ return Runtime_Windows.PySequence_Repeat(pointer, count); }


        public int PySequence_Index(IntPtr pointer, IntPtr item){ return Runtime_Windows.PySequence_Index(pointer, item); }


        public int PySequence_Count(IntPtr pointer, IntPtr value){ return Runtime_Windows.PySequence_Count(pointer, value); }


        public IntPtr PySequence_Tuple(IntPtr pointer){ return Runtime_Windows.PySequence_Tuple(pointer); }


        public IntPtr PySequence_List(IntPtr pointer){ return Runtime_Windows.PySequence_List(pointer); }

#if PYTHON3

        public IntPtr PyBytes_FromString(string op){ return Runtime_Windows.PyBytes_FromString(op); }


        public int PyBytes_Size(IntPtr op){ return Runtime_Windows.PyBytes_Size(op); }


        public IntPtr PyString_FromStringAndSize(
             string value,
            int size
        ){ return Runtime_Windows.PyString_FromStringAndSize(value, size); }


        public IntPtr PyUnicode_FromStringAndSize(IntPtr value, int size){ return Runtime_Windows.PyUnicode_FromStringAndSize(value, size); }
#elif PYTHON2
        
        public  IntPtr PyString_FromStringAndSize(string value, int size){ return Runtime_Windows.PyUnicode_FromStringAndSize(value, size); }

        
        public  IntPtr PyString_AsString(IntPtr op){ return Runtime_Windows.PyString_AsString(op); }

        
        public  int PyString_Size(IntPtr pointer){ return Runtime_Windows.PyString_Size(pointer); }
#endif


#if PYTHON3

        public IntPtr PyUnicode_FromObject(IntPtr ob){ return Runtime_Windows.PyUnicode_FromObject(ob); }


        public IntPtr PyUnicode_FromEncodedObject(IntPtr ob, IntPtr enc, IntPtr err){ return Runtime_Windows.PyUnicode_FromEncodedObject(ob, enc, err); }


        public IntPtr PyUnicode_FromKindAndData(
            int kind,
            string s,
            int size
        ){ return Runtime_Windows.PyUnicode_FromKindAndData(kind, s, size); }


        public int PyUnicode_GetSize(IntPtr ob){ return Runtime_Windows.PyUnicode_GetSize(ob); }


        public IntPtr PyUnicode_AsUnicode(IntPtr ob){ return Runtime_Windows.PyUnicode_AsUnicode(ob); }


        public IntPtr PyUnicode_FromOrdinal(int c){ return Runtime_Windows.PyUnicode_FromOrdinal(c); }
#elif PYTHON2
        public  IntPtr PyUnicode_FromObject(IntPtr ob){ return Runtime_Windows.PyUnicode_FromObject(ob); }

        public  IntPtr PyUnicode_FromEncodedObject(IntPtr ob, IntPtr enc, IntPtr err){ return Runtime_Windows.PyUnicode_FromEncodedObject(ob, enc, err); }

        public  IntPtr PyUnicode_FromUnicode(
            string s,
            int size
        ){ return Runtime_Windows.PyUnicode_FromUnicode(s, size); }

        public  int PyUnicode_GetSize(IntPtr ob){ return Runtime_Windows.PyUnicode_GetSize(ob); }

        public  IntPtr PyUnicode_AsUnicode(IntPtr ob){ return Runtime_Windows.PyUnicode_AsUnicode(ob); }

        public  IntPtr PyUnicode_FromOrdinal(int c){ return Runtime_Windows.PyUnicode_FromOrdinal(c); }
#endif




        public IntPtr PyDict_New(){ return Runtime_Windows.PyDict_New(); }


        public IntPtr PyDictProxy_New(IntPtr dict){ return Runtime_Windows.PyDictProxy_New(dict); }


        public IntPtr PyDict_GetItem(IntPtr pointer, IntPtr key){ return Runtime_Windows.PyDict_GetItem(pointer, key); }


        public IntPtr PyDict_GetItemString(IntPtr pointer, string key){ return Runtime_Windows.PyDict_GetItemString(pointer, key); }


        public int PyDict_SetItem(IntPtr pointer, IntPtr key, IntPtr value){ return Runtime_Windows.PyDict_SetItem(pointer, key, value); }


        public int PyDict_SetItemString(IntPtr pointer, string key, IntPtr value){ return Runtime_Windows.PyDict_SetItemString(pointer, key, value); }


        public int PyDict_DelItem(IntPtr pointer, IntPtr key){ return Runtime_Windows.PyDict_DelItem(pointer, key); }


        public int PyDict_DelItemString(IntPtr pointer, string key){ return Runtime_Windows.PyDict_DelItemString(pointer, key); }


        public int PyMapping_HasKey(IntPtr pointer, IntPtr key){ return Runtime_Windows.PyMapping_HasKey(pointer, key); }


        public IntPtr PyDict_Keys(IntPtr pointer){ return Runtime_Windows.PyDict_Keys(pointer); }


        public IntPtr PyDict_Values(IntPtr pointer){ return Runtime_Windows.PyDict_Values(pointer); }


        public IntPtr PyDict_Items(IntPtr pointer){ return Runtime_Windows.PyDict_Items(pointer); }


        public IntPtr PyDict_Copy(IntPtr pointer){ return Runtime_Windows.PyDict_Copy(pointer); }


        public int PyDict_Update(IntPtr pointer, IntPtr other){ return Runtime_Windows.PyDict_Update(pointer, other); }


        public void PyDict_Clear(IntPtr pointer){  Runtime_Windows.PyDict_Clear(pointer); }


        public int PyDict_Size(IntPtr pointer){ return Runtime_Windows.PyDict_Size(pointer); }





        public IntPtr PyList_New(int size){ return Runtime_Windows.PyList_New(size); }


        public IntPtr PyList_AsTuple(IntPtr pointer){ return Runtime_Windows.PyList_AsTuple(pointer); }


        public IntPtr PyList_GetItem(IntPtr pointer, int index){ return Runtime_Windows.PyList_GetItem(pointer, index); }


        public int PyList_SetItem(IntPtr pointer, int index, IntPtr value){ return Runtime_Windows.PyList_SetItem(pointer, index, value); }


        public int PyList_Insert(IntPtr pointer, int index, IntPtr value){ return Runtime_Windows.PyList_Insert(pointer, index, value); }


        public int PyList_Append(IntPtr pointer, IntPtr value){ return Runtime_Windows.PyList_Append(pointer, value); }


        public int PyList_Reverse(IntPtr pointer){ return Runtime_Windows.PyList_Reverse(pointer); }


        public int PyList_Sort(IntPtr pointer){ return Runtime_Windows.PyList_Sort(pointer); }


        public IntPtr PyList_GetSlice(IntPtr pointer, int start, int end){ return Runtime_Windows.PyList_GetSlice(pointer, start, end); }


        public int PyList_SetSlice(IntPtr pointer, int start, int end, IntPtr value){ return Runtime_Windows.PyList_SetSlice(pointer, start, end, value); }


        public int PyList_Size(IntPtr pointer){ return Runtime_Windows.PyList_Size(pointer); }



        public IntPtr PyTuple_New(int size){ return Runtime_Windows.PyTuple_New(size); }


        public IntPtr PyTuple_GetItem(IntPtr pointer, int index){ return Runtime_Windows.PyTuple_GetItem(pointer, index); }


        public int PyTuple_SetItem(IntPtr pointer, int index, IntPtr value){ return Runtime_Windows.PyTuple_SetItem(pointer, index, value); }


        public IntPtr PyTuple_GetSlice(IntPtr pointer, int start, int end){ return Runtime_Windows.PyTuple_GetSlice(pointer, start, end); }


        public int PyTuple_Size(IntPtr pointer){ return Runtime_Windows.PyTuple_Size(pointer); }





        public IntPtr PyIter_Next(IntPtr pointer){ return Runtime_Windows.PyIter_Next(pointer); }


        //====================================================================
        // Python module API
        //====================================================================


        public IntPtr PyModule_New(string name){ return Runtime_Windows.PyModule_New(name); }


        public string PyModule_GetName(IntPtr module){ return Runtime_Windows.PyModule_GetName(module); }


        public IntPtr PyModule_GetDict(IntPtr module){ return Runtime_Windows.PyModule_GetDict(module); }


        public string PyModule_GetFilename(IntPtr module){ return Runtime_Windows.PyModule_GetFilename(module); }

#if PYTHON3

        public IntPtr PyModule_Create2(IntPtr module, int apiver){ return Runtime_Windows.PyModule_Create2(module, apiver); }
#endif


        public IntPtr PyImport_Import(IntPtr name){ return Runtime_Windows.PyImport_Import(name); }


        public IntPtr PyImport_ImportModule(string name){ return Runtime_Windows.PyImport_ImportModule(name); }


        public IntPtr PyImport_ReloadModule(IntPtr module){ return Runtime_Windows.PyImport_ReloadModule(module); }


        public IntPtr PyImport_AddModule(string name){ return Runtime_Windows.PyImport_AddModule(name); }


        public IntPtr PyImport_GetModuleDict(){ return Runtime_Windows.PyImport_GetModuleDict(); }

#if PYTHON3

        public void PySys_SetArgvEx(
            int argc,
            string[] argv,
            int updatepath
        ){ Runtime_Windows.PySys_SetArgvEx(argc,
            argv,
            updatepath); }
#elif PYTHON2
        
        public  void PySys_SetArgvEx(
            int argc,
            string[] argv,
            int updatepath
        ){ return Runtime_Windows.PySys_SetArgvEx(argc,
            argv,
            updatepath); }
#endif


        public IntPtr PySys_GetObject(string name){ return Runtime_Windows.PySys_GetObject(name); }


        public int PySys_SetObject(string name, IntPtr ob){ return Runtime_Windows.PySys_SetObject(name, ob); }



        public void PyType_Modified(IntPtr type){ Runtime_Windows.PyType_Modified(type); }


        public bool PyType_IsSubtype(IntPtr t1, IntPtr t2){ return Runtime_Windows.PyType_IsSubtype(t1, t2); }




        public IntPtr PyType_GenericNew(IntPtr type, IntPtr args, IntPtr kw){ return Runtime_Windows.PyType_GenericNew(type, args, kw); }


        public IntPtr PyType_GenericAlloc(IntPtr type, int n){ return Runtime_Windows.PyType_GenericAlloc(type, n); }


        public int PyType_Ready(IntPtr type){ return Runtime_Windows.PyType_Ready(type); }


        public IntPtr _PyType_Lookup(IntPtr type, IntPtr name){ return Runtime_Windows._PyType_Lookup(type, name); }


        public IntPtr PyObject_GenericGetAttr(IntPtr obj, IntPtr name){ return Runtime_Windows.PyObject_GenericGetAttr(obj, name); }


        public int PyObject_GenericSetAttr(IntPtr obj, IntPtr name, IntPtr value){ return Runtime_Windows.PyObject_GenericSetAttr(obj, name, value); }


        public IntPtr _PyObject_GetDictPtr(IntPtr obj){ return Runtime_Windows._PyObject_GetDictPtr(obj); }


        public IntPtr PyObject_GC_New(IntPtr tp){ return Runtime_Windows.PyObject_GC_New(tp); }


        public void PyObject_GC_Del(IntPtr tp){  Runtime_Windows.PyObject_GC_Del(tp); }


        public void PyObject_GC_Track(IntPtr tp){  Runtime_Windows.PyObject_GC_Track(tp); }


        public void PyObject_GC_UnTrack(IntPtr tp){  Runtime_Windows.PyObject_GC_UnTrack(tp); }


        //====================================================================
        // Python memory API
        //====================================================================


        public IntPtr PyMem_Malloc(int size){ return Runtime_Windows.PyMem_Malloc(size); }


        public IntPtr PyMem_Realloc(IntPtr ptr, int size){ return Runtime_Windows.PyMem_Realloc(ptr, size); }


        public void PyMem_Free(IntPtr ptr){  Runtime_Windows.PyMem_Free(ptr); }


        //====================================================================
        // Python exception API
        //====================================================================


        public void PyErr_SetString(IntPtr ob, string message){  Runtime_Windows.PyErr_SetString(ob, message); }


        public void PyErr_SetObject(IntPtr ob, IntPtr message){ Runtime_Windows.PyErr_SetObject(ob, message); }


        public IntPtr PyErr_SetFromErrno(IntPtr ob){ return Runtime_Windows.PyErr_SetFromErrno(ob); }


        public void PyErr_SetNone(IntPtr ob){  Runtime_Windows.PyErr_SetNone(ob); }


        public int PyErr_ExceptionMatches(IntPtr exception){ return Runtime_Windows.PyErr_ExceptionMatches(exception); }


        public int PyErr_GivenExceptionMatches(IntPtr ob, IntPtr val){ return Runtime_Windows.PyErr_GivenExceptionMatches(ob, val); }


        public void PyErr_NormalizeException(IntPtr ob, IntPtr val, IntPtr tb){  Runtime_Windows.PyErr_NormalizeException(ob, val, tb); }


        public int PyErr_Occurred(){ return Runtime_Windows.PyErr_Occurred(); }


        public void PyErr_Fetch(ref IntPtr ob, ref IntPtr val, ref IntPtr tb){ Runtime_Windows.PyErr_Fetch(ref ob, ref val, ref tb); }


        public void PyErr_Restore(IntPtr ob, IntPtr val, IntPtr tb){  Runtime_Windows.PyErr_Restore(ob, val, tb); }


        public void PyErr_Clear(){  Runtime_Windows.PyErr_Clear(); }


        public void PyErr_Print(){  Runtime_Windows.PyErr_Print(); }


        //====================================================================
        // Miscellaneous
        //====================================================================


        public IntPtr PyMethod_Self(IntPtr ob){ return Runtime_Windows.PyMethod_Self(ob); }


        public IntPtr PyMethod_Function(IntPtr ob){ return Runtime_Windows.PyMethod_Function(ob); }


    }
}
