// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include "windows.h"
#include "Strsafe.h"
#include "mscoree.h"

#define BUFSIZE MAX_PATH

void netclr()
{
LPWSTR Buffer=new TCHAR[BUFSIZE];
ICLRRuntimeHost* pCLR = NULL;
DWORD result;

GetCurrentDirectory(BUFSIZE, Buffer);
lstrcatW(Buffer,L"\\vhook.dll");

   // start the .NET Runtime in the current native process
   CorBindToRuntimeEx(NULL, L"wks", NULL, CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, (LPVOID*)&pCLR);

   pCLR->Start();

 //Fourth Param is dummy and also the fifth
   pCLR->ExecuteInDefaultAppDomain(Buffer, L"VHook.HookTest.clsHook", L"InjectHook", L"Simon-Benyo", &result);

}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	CreateThread(NULL,NULL,(LPTHREAD_START_ROUTINE)netclr,NULL,NULL,NULL);
   break;
	case DLL_THREAD_ATTACH:
		break;
	case DLL_THREAD_DETACH:
		break;
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

