# High-Performance Native Interop Engine (C / C++ / C#)

![Build & Test](https://github.com/Med12334/native-suite/actions/workflows/ci.yml/badge.svg)

A multi-tiered systems architecture demonstrating high-throughput, zero-copy interop between managed .NET and unmanaged native runtimes.

## Architecture

- **Layer 4: Presentation & Orchestration (C# / .NET 8)**
  - SafeHandle unmanaged memory lifecycle management
  - Zero-allocation `fixed` pointer pinning
- **Layer 3: C ABI Bridge (extern "C")**
  - Name-demangled export boundary (`libnative_core.so`)
  - Opaque pointer context mapping
- **Layer 2: Core Engine (Modern C++20)**
  - RAII, smart pointers (`std::unique_ptr`), atomic state tracking
- **Layer 1: Hardware & Low-Level Primitives (C11)**
  - 64-bit FNV-1a bitwise stream hashing
  - Zero runtime overhead

## Build Instructions

### 1. Build Native Shared Library
```bash
cmake -B build
cmake --build build
