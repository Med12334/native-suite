#include "bridge_api.h"
#include "engine_core.hpp"
#include <memory>

struct EngineContext {
    std::unique_ptr<CoreEngine> engine;
};

extern "C" {

EngineContext* engine_create(void) {
    auto ctx = new EngineContext();
    ctx->engine = std::make_unique<CoreEngine>();
    return ctx;
}

void engine_destroy(EngineContext* engine) {
    delete engine;
}

int32_t engine_get_version(void) {
    return 200;
}

uint64_t engine_hash_buffer(EngineContext* engine, const uint8_t* data, size_t len) {
    if (!engine || !engine->engine) return 0;
    return engine->engine->process_stream(data, len);
}

uint64_t engine_get_metrics(EngineContext* engine) {
    if (!engine || !engine->engine) return 0;
    return engine->engine->get_total_processed();
}

}
