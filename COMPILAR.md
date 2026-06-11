# 📦 Cómo Compilar y Publicar LOTERIAMX1

## 🎯 Objetivo
Generar una carpeta con el `.exe` listo para usar sin necesidad de instalación.

## 📋 Requisitos
- **.NET SDK 6.0 o superior** instalado
- **Visual Studio** (opcional, puedes usar línea de comandos)

## 🚀 Pasos para Compilar

### **Opción 1: Desde Línea de Comandos (Recomendado)**

```bash
# 1. Abre PowerShell o CMD en la carpeta del proyecto
cd C:\ruta\a\tu\proyecto\LOTERIAMX1

# 2. Compila en modo Release (optimizado)
dotnet publish -c Release -r win-x64 --self-contained

# 3. ¡Listo! La carpeta con el .exe se crea automáticamente en:
# bin\Release\net6.0-windows\win-x64\publish
```

### **Opción 2: Desde Visual Studio**

1. Abre tu proyecto en Visual Studio
2. **Build** → **Publish LOTERIAMX1**
3. Selecciona **Folder**
4. Elige una carpeta de destino
5. Click en **Publish**

## 📁 Estructura de la Carpeta Generada

```
publish/
├── LOTERIAMX1.exe          ← ¡Este es tu ejecutable!
├── LOTERIAMX1.dll
├── appsettings.json
├── webview2loader.dll
└── ... (otras dependencias)
```

## ✅ Cómo Usar el .exe

1. **Copia la carpeta `publish`** a donde quieras
2. **Doble-click en `LOTERIAMX1.exe`**
3. ¡La aplicación se abre!

## 🎁 Distribuir tu Aplicación

### **Opción A: Compartir la Carpeta Completa**
```
LOTERIAMX1/
├── LOTERIAMX1.exe
├── LOTERIAMX1.dll
├── ... (todas las dependencias)
```
Comprime como `.zip` y comparte.

### **Opción B: Subir a GitHub Releases**

1. Ve a https://github.com/kikivallesteros-cyber/LOTERIAMX1
2. Click en **Releases**
3. Click en **Create a new release**
4. Comprime la carpeta `publish` como `LOTERIAMX1-v1.0.zip`
5. Sube el .zip
6. Comparte el link con amigos

## 🐛 Si No Te Funciona

**Error: "No se puede encontrar .NET"**
```bash
# Instala .NET Runtime:
# Descarga desde: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
```

**Error: "netcoreapp6.0-windows"**
```bash
# Compila como:
dotnet publish -c Release -r win-x64 --self-contained true
```

## 💡 Comandos Útiles

```bash
# Ver versión de .NET instalada
dotnet --version

# Compilar en modo Debug (rápido, no optimizado)
dotnet publish -c Debug

# Compilar solo para tu PC (sin -r win-x64)
dotnet publish -c Release
```

## 🎉 ¡Listo!

Ya tienes tu `.exe` compilado y listo para compartir con tus amigos.

---

**¿Necesitas ayuda?** Pregunta en los Issues del repositorio.
