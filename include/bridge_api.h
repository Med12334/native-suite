#ifndef BRIDGE_API_H
#define BRIDGE_API_H

#include <stdint.h>
#include <stddef.h>

#ifdef __cplusplus
extern "C" {
#endif

typedef struct EngineContext EngineContext;

EngineContext* engine_create(void);
void engine_destroy(EngineContext* engine);
int32_t engine_get_version(void);
uint64_t engine_hash_buffer(EngineContext* engine, const uint8_t* data, size_t len);
uint64_t engine_get_metrics(EngineContext* engine);

#ifdef __cplusplus
}
#endif

#endif
