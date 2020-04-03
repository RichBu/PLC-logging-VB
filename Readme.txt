HEI_PAS.Dll needs to be in the same folder as HEI32_3.DLL, usually C:\Windows\Systems32.



If you get the error message about MSSTDFMT.DLL not being registered do the following:

copy MSSTDFMT.DLL to the \Winnt\System32 folder
use Start->Run... RegSvr32 C:\Winnt\System32\MSSTDFMT.DLL to register the DLL