#pragma once
#include <cstdint>
#include <cstddef>

extern "C" uint32_t c_buffer_checksum(const uint8_t* data, size_t len);

class CoreEngine {
public:
    int32_t get_version() const noexcept { return 100; }

    uint32_t process_stream(const uint8_t* buffer, size_t length) {
        return c_buffer_checksum(buffer, length);
    }
};
