IF EXIST bin\x86\Debug\net8.0-windows\Data\ (
    mkdir bin\x86\Debug\net8.0-windows\Data 
    xcopy /E /I Data bin\x86\Debug\net8.0-windows\Data\
) ELSE (
    xcopy /E /I Data bin\x86\Debug\net8.0-windows\Data\
)
IF EXIST bin\x86\Release (
    IF NOT EXIST bin\x86\Release\net8.0-windows\Data\ (
        mkdir bin\x86\Release\net8.0-windows\Data 
        xcopy /E /I Data bin\x86\Release\net8.0-windows\Data\
    ) ELSE (
        xcopy /E /I Data bin\x86\Release\net8.0-windows\Data\
    )
)
IF EXIST bin\x64\Debug\net8.0-windows\Data\ (
    mkdir bin\x64\Debug\net8.0-windows\Data 
    xcopy /E /I Data bin\x86\Debug\net8.0-windows\Data\
) ELSE (
    xcopy /E /I Data bin\x64\Debug\net8.0-windows\Data\
)
IF EXIST bin\x64\Release (
    IF NOT EXIST bin\x64\Release\net8.0-windows\Data\ (
        mkdir bin\x64\Release\net8.0-windows\Data 
        xcopy /E /I Data bin\x64\Release\net8.0-windows\Data\
    ) ELSE (
        xcopy /E /I Data bin\x64\Release\net8.0-windows\Data\
    )
)
IF EXIST bin\Debug\net8.0-windows\Data\ (
    mkdir bin\Debug\net8.0-windows\Data 
    xcopy /E /I Data bin\Debug\net8.0-windows\Data\
) ELSE (
    xcopy /E /I Data bin\Debug\net8.0-windows\Data\
)
IF EXIST bin\Release (
    IF NOT EXIST bin\Release\net8.0-windows\Data\ (
        mkdir bin\Release\net8.0-windows\Data 
        xcopy /E /I Data bin\Release\net8.0-windows\Data\
    ) ELSE (
        xcopy /E /I Data bin\Release\net8.0-windows\Data\
    )
)