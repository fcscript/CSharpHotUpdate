LOCAL_PATH := $(call my-dir)

include $(CLEAR_VARS)

#https://developer.android.com/ndk/guides/android_mk
#$(call import-add-path,$(LOCAL_PATH)/../)
#$(call import-add-path,$(LOCAL_PATH)/../net)

LOCAL_MODULE := fclib_dll

LOCAL_SRC_FILES := ../class_base.cpp \
                   ../fc_any_hash_map.cpp \
                   ../fc_api.cpp \
                   ../fc_class_ins.cpp \
                   ../fc_code_mng.cpp \
                   ../fc_coroutine.cpp \
                   ../fc_coroutine_mng.cpp \
                   ../fc_delegate.cpp \
                   ../fc_hash_map.cpp \
                   ../fc_init_value.cpp \
                   ../fc_print.cpp \
                   ../fc_malloc.cpp \
                   ../fc_ref_object.cpp \
                   ../fc_delay_check.cpp \
                   ../fc_broadcast_mng.cpp \
                   ../fc_static_buffer_inport_class.cpp \
                   ../fc_stack_buffer.cpp \
                   ../fc_stack_buffer_excute.cpp \
                   ../fc_stack_buffer_excute_call.cpp \
                   ../fc_stack_buffer_excute_debug.cpp \
                   ../fc_static_array.cpp \
                   ../fc_static_buffer_array.cpp \
                   ../fc_static_buffer_coroutine.cpp \
                   ../fc_static_buffer_graphic.cpp \
                   ../fc_static_buffer_map.cpp \
                   ../fc_static_buffer_string_a.cpp \
                   ../fc_static_buffer_string_w.cpp \
                   ../fc_static_buffer_system.cpp \
                   ../fc_static_buffer_color.cpp \
                   ../fc_string.cpp \
                   ../fc_xml.cpp \
                   ../fc_json.cpp \
				   ../ExcelTable.cpp \
				   ../fc_excel.cpp \
                   ../C_API.cpp \
                   ../UTF8.cpp \
				   ../fc_binary.cpp \
                   ../net/fc_tcp_server.cpp \




#LOCAL_SHARED_LIBRARIES := fclib_dll

include $(BUILD_SHARED_LIBRARY)


