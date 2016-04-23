#pragma once
using namespace System::Runtime::InteropServices;

template<typename M, typename N>
M NToM(N* pNative)
{
	return safe_cast<M>(Marshal::PtrToStructure(IntPtr(pNative), M::typeid));
}

template<typename M, typename N>
void MToN(M managed, N* pNative)
{
	Marshal::StructureToPtr(managed, IntPtr(pNative), true);
};

class AutoStrPtr
{
public:
	char* m_pChar;

	AutoStrPtr(String^ str)
	{
		if (str != nullptr)
		{
			m_pChar = (char*)Marshal::StringToHGlobalAnsi(str).ToPointer();
		}
		else
		{
			m_pChar = nullptr;
		}
	}

	~AutoStrPtr()
	{
		if (m_pChar != nullptr)
		{
			Marshal::FreeHGlobal(IntPtr(m_pChar));
		}
	}
};

class AutoStrArrayPtr
{
private:
	int length;
public:
	char** ppChar;

	AutoStrArrayPtr(array<String^>^ ppInstrumentID)
	{
		length = ppInstrumentID->Length;
		ppChar = new char*[length];
		for (int i = 0; i < length; i++)
		{
			ppChar[i] = (char*)Marshal::StringToHGlobalAnsi(ppInstrumentID[i]).ToPointer();
		}
	}
	~AutoStrArrayPtr()
	{
		for (int i = 0; i < length; i++)
		{
			Marshal::FreeHGlobal(IntPtr(ppChar[i]));
		}
		delete[length] ppChar;
	}
};