call "C:\Program Files\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"
devenv /build Release Schlage.sln
rmdir release\ /s
mkdir release
mkdir release\100_entrada
mkdir release\100_salida
mkdir release\200_entrada
mkdir release\200_salida
mkdir release\300_entrada
mkdir release\300_salida
mkdir release\400_entrada
mkdir release\400_salida
mkdir release\500_entrada
mkdir release\500_salida
mkdir release\600_entrada
mkdir release\600_salida
mkdir release\AdminProd

copy "Lineas\bin\Release\Lineas.exe" release\100_entrada\LineaEntrada.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\100_entrada\ /y
copy "Lineas\bin\Release\C5.dll" release\100_entrada\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\100_entrada\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\100_entrada\ /y

copy "Lineas\bin\Release\Lineas.exe" release\100_salida\LineaSalida.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\100_salida\ /y
copy "Lineas\bin\Release\C5.dll" release\100_salida\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\100_salida\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\100_salida\ /y

copy "Lineas\bin\Release\Lineas.exe" release\200_entrada\LineaEntrada.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\200_entrada\ /y
copy "Lineas\bin\Release\C5.dll" release\200_entrada\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\200_entrada\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\200_entrada\ /y

copy "Lineas\bin\Release\Lineas.exe" release\200_salida\LineaSalida.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\200_salida\ /y
copy "Lineas\bin\Release\C5.dll" release\200_salida\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\200_salida\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\200_salida\ /y

copy "Lineas\bin\Release\Lineas.exe" release\300_entrada\LineaEntrada.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\300_entrada\ /y
copy "Lineas\bin\Release\C5.dll" release\300_entrada\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\300_entrada\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\300_entrada\ /y

copy "Lineas\bin\Release\Lineas.exe" release\300_salida\LineaSalida.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\300_salida\ /y
copy "Lineas\bin\Release\C5.dll" release\300_salida\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\300_salida\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\300_salida\ /y

copy "Lineas\bin\Release\Lineas.exe" release\400_entrada\LineaEntrada.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\400_entrada\ /y
copy "Lineas\bin\Release\C5.dll" release\400_entrada\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\400_entrada\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\400_entrada\ /y

copy "Lineas\bin\Release\Lineas.exe" release\400_salida\LineaSalida.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\400_salida\ /y
copy "Lineas\bin\Release\C5.dll" release\400_salida\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\400_salida\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\400_salida\ /y

copy "Lineas\bin\Release\Lineas.exe" release\500_entrada\LineaEntrada.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\500_entrada\ /y
copy "Lineas\bin\Release\C5.dll" release\500_entrada\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\500_entrada\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\500_entrada\ /y

copy "Lineas\bin\Release\Lineas.exe" release\500_salida\LineaSalida.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\500_salida\ /y
copy "Lineas\bin\Release\C5.dll" release\500_salida\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\500_salida\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\500_salida\ /y

copy "Lineas\bin\Release\Lineas.exe" release\600_entrada\LineaEntrada.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\600_entrada\ /y
copy "Lineas\bin\Release\C5.dll" release\600_entrada\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\600_entrada\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\600_entrada\ /y

copy "Lineas\bin\Release\Lineas.exe" release\600_salida\LineaSalida.exe /y
copy "Lineas\bin\Release\Interop.OPCAutomation.dll" release\600_salida\ /y
copy "Lineas\bin\Release\C5.dll" release\600_salida\ /y
copy "Lineas\bin\Release\Newtonsoft.Json.dll" release\600_salida\ /y
copy "Lineas\bin\Release\Plossum CommandLine.dll" release\600_salida\ /y

copy "Administrador\bin\Release\Administrador.exe" release\AdminProd\ /y
copy "Administrador\s_productos.cmd" release\AdminProd\ /y

copy "configs\100_entrada.json" release\100_entrada\configuration.json /y
copy "configs\100_salida.json" release\100_salida\configuration.json /y
copy "configs\200_entrada.json" release\200_entrada\configuration.json /y
copy "configs\200_salida.json" release\200_salida\configuration.json /y
copy "configs\300_entrada.json" release\300_entrada\configuration.json /y
copy "configs\300_salida.json" release\300_salida\configuration.json /y
copy "configs\400_entrada.json" release\400_entrada\configuration.json /y
copy "configs\400_salida.json" release\400_salida\configuration.json /y
copy "configs\500_entrada.json" release\500_entrada\configuration.json /y
copy "configs\500_salida.json" release\500_salida\configuration.json /y
copy "configs\600_entrada.json" release\600_entrada\configuration.json /y
copy "configs\600_salida.json" release\600_salida\configuration.json /y
copy "configs\Administrador.exe.config" release\AdminProd\Administrador.exe.config /y
pause