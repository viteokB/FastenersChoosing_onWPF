IF NOT EXIST bin\x86\Debug\net8.0-windows\Data\ (
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