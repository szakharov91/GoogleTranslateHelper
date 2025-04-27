@echo off
echo → Запуск сборки и анализаторов...
dotnet build --no-restore
if errorlevel 1 (
  echo ✗ Ошибки сборки/анализа — пуш отменён.
  exit /b 1
)