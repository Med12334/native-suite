#include <stdint.h>
#include <stddef.h>

#define FNV_OFFSET_BASIS 14695981039346656037ULL
#define FNV_PRIME        1099511628211ULL

// Fast bitwise 64-bit hashing primitive (Layer 1)
uint64_t c_fnv1a_hash(const uint8_t* data, size_t len) {
    if (!data) return 0;
    
    uint64_t hash = FNV_OFFSET_BASIS;
    for (size_t i = 0; i < len; ++i) {
        hash ^= (uint64_t)data[i];
        hash *= FNV_PRIME;
    }
    return hash;
}
