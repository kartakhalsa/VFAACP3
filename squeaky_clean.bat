@echo off
echo squeaky_clean.bat

rem This batch file removes generated folders and files.
rem Run this after performing Visual Studio Clean.

call :remove_folder VFAACP\bin\Debug\de
call :remove_folder VFAACP\bin\Debug\es
call :remove_folder VFAACP\bin\Debug\ja
call :remove_folder VFAACP\bin\Debug\ru
call :remove_folder VFAACP\bin\Debug

call :remove_folder VFAACP\bin\Release\de
call :remove_folder VFAACP\bin\Release\es
call :remove_folder VFAACP\bin\Release\ja
call :remove_folder VFAACP\bin\Release\ru
call :remove_folder VFAACP\bin\Release

call :remove_folder VFAACP\bin

call :remove_folder VFAACP\obj\Debug\TempPE
call :remove_folder VFAACP\obj\Debug
call :remove_folder VFAACP\obj\Release\TempPE
call :remove_folder VFAACP\obj\Release

call :remove_folder VFAACP\obj

goto :exit

:remove_folder
set folder=%1
if not exist %folder%\. goto :eof
echo Removing folder %folder%
del /Q %folder%\*
rd /S /Q %folder%
goto :eof

:remove_file
set file=%1
if not exist %file% goto :eof
echo Removing file %file%
del %file%
goto :eof

:exit
echo.
