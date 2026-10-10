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
uint32_t engine_compute(EngineContext* engine, const uint8_t* data, size_t len);

#ifdef __cplusplus
}
#endif

#endif
