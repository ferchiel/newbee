@echo off
rem ╡Иурнд╪Ч
for /f "delims=" %%i in ('dir /b "proto\*.proto"') do echo %%i
for /f "delims=" %%i in ('dir /b/a "proto\*.proto"') do protogen -i:proto\%%i -o:.\out\%%~ni.cs
pause