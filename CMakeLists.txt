cmake_minimum_required(VERSION 3.20)
project(NativeSuite LANGUAGES C CXX)

set(CMAKE_C_STANDARD 11)
set(CMAKE_CXX_STANDARD 20)
include_directories(include)

set(SOURCES
    src/layer1_c/raw_buffer.c
    src/layer2_cpp/engine_core.cpp
    src/layer3_bridge/bridge_api.cpp
)

add_library(native_core SHARED ${SOURCES})
