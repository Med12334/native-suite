# High-Performance Native Interop Engine (C / C++ / C#)

Architecture multi-couches démontrant une interopérabilité haute performance et sans copie (zero-copy) entre le runtime managé .NET et le code natif non managé.

## Architecture

- **Layer 4 : C# / .NET 8**
  - Gestion du cycle de vie mémoire via `SafeHandle`
  - Épinglage de mémoire sans allocation (`fixed` byte*)
- **Layer 3 : Pont C ABI (extern "C")**
  - Exportation dynamique standardisée (`libnative_core.so`)
  - Masquage des objets C++ sous forme de pointeurs opaques
- **Layer 2 : Moteur C++20**
  - RAII, pointeurs intelligents (`std::unique_ptr`), compteurs atomiques
- **Layer 1 : Primitives C11**
  - Algorithme de hachage 64-bit FNV-1a au niveau bit
  - Zéro surcharge d'exécution

## Compilation & Exécution

```bash
cmake -B build
cmake --build build
cd bindings/csharp
dotnet run
