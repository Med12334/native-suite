#pragma once
#include <cstdint>
#include <cstddef>
#include <atomic>

extern "C" uint64_t c_fnv1a_hash(const uint8_t* data, size_t len);

class CoreEngine {
private:
    std::atomic<uint64_t> m_total_bytes_processed{0};

public:
    int32_t get_version() const noexcept { return 200; }

    uint64_t process_stream(const uint8_t* buffer, size_t length) {
        if (!buffer || length == 0) return 0;
        
        m_total_bytes_processed.fetch_add(length, std::memory_order_relaxed);
        return c_fnv1a_hash(buffer, length);
    }

    uint64_t get_total_processed() const noexcept {
        return m_total_bytes_processed.load(std::memory_order_relaxed);
    }
};
