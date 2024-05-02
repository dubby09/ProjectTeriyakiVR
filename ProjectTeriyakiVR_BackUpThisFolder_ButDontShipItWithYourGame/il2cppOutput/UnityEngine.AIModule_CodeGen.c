#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.Boolean UnityEngine.AI.NavMeshAgent::SetDestination(UnityEngine.Vector3)
extern void NavMeshAgent_SetDestination_mD5D960933827F1F14B29CF4A3B6F305C064EBF46 (void);
// 0x00000002 System.Boolean UnityEngine.AI.NavMeshAgent::get_isStopped()
extern void NavMeshAgent_get_isStopped_m1CF52CE58CC025C1617F9E745924624C35D0B598 (void);
// 0x00000003 System.Void UnityEngine.AI.NavMeshAgent::set_speed(System.Single)
extern void NavMeshAgent_set_speed_m820E45289B3AE7DEE16F2F4BF163EAC361E64646 (void);
// 0x00000004 System.Boolean UnityEngine.AI.NavMeshAgent::SetDestination_Injected(UnityEngine.Vector3&)
extern void NavMeshAgent_SetDestination_Injected_mC3EF405F5AAFF9F98C5D5AECAD641525CDF742EA (void);
// 0x00000005 UnityEngine.Vector3 UnityEngine.AI.NavMeshHit::get_position()
extern void NavMeshHit_get_position_m09E8FF6DEF5BFA3F30B3C4BCA4642442FF1BCBF1 (void);
// 0x00000006 System.Void UnityEngine.AI.NavMesh::Internal_CallOnNavMeshPreUpdate()
extern void NavMesh_Internal_CallOnNavMeshPreUpdate_m2A62DB32F5E1435F527AD8A59A882B9F2A193177 (void);
// 0x00000007 System.Boolean UnityEngine.AI.NavMesh::SamplePosition(UnityEngine.Vector3,UnityEngine.AI.NavMeshHit&,System.Single,System.Int32)
extern void NavMesh_SamplePosition_mEDAE8240217EED82B4519D4435E670A1655B2109 (void);
// 0x00000008 System.Boolean UnityEngine.AI.NavMesh::SamplePosition_Injected(UnityEngine.Vector3&,UnityEngine.AI.NavMeshHit&,System.Single,System.Int32)
extern void NavMesh_SamplePosition_Injected_m57BB80634365C0545A79CC653FCFECAA1879C6F0 (void);
// 0x00000009 System.Void UnityEngine.AI.NavMesh/OnNavMeshPreUpdate::.ctor(System.Object,System.IntPtr)
extern void OnNavMeshPreUpdate__ctor_m7142A3AA991BE50B637A16D946AB7604C64EF9BA (void);
// 0x0000000A System.Void UnityEngine.AI.NavMesh/OnNavMeshPreUpdate::Invoke()
extern void OnNavMeshPreUpdate_Invoke_mFB224B9BBF9C78B7F39AA91A047F175C69897914 (void);
static Il2CppMethodPointer s_methodPointers[10] = 
{
	NavMeshAgent_SetDestination_mD5D960933827F1F14B29CF4A3B6F305C064EBF46,
	NavMeshAgent_get_isStopped_m1CF52CE58CC025C1617F9E745924624C35D0B598,
	NavMeshAgent_set_speed_m820E45289B3AE7DEE16F2F4BF163EAC361E64646,
	NavMeshAgent_SetDestination_Injected_mC3EF405F5AAFF9F98C5D5AECAD641525CDF742EA,
	NavMeshHit_get_position_m09E8FF6DEF5BFA3F30B3C4BCA4642442FF1BCBF1,
	NavMesh_Internal_CallOnNavMeshPreUpdate_m2A62DB32F5E1435F527AD8A59A882B9F2A193177,
	NavMesh_SamplePosition_mEDAE8240217EED82B4519D4435E670A1655B2109,
	NavMesh_SamplePosition_Injected_m57BB80634365C0545A79CC653FCFECAA1879C6F0,
	OnNavMeshPreUpdate__ctor_m7142A3AA991BE50B637A16D946AB7604C64EF9BA,
	OnNavMeshPreUpdate_Invoke_mFB224B9BBF9C78B7F39AA91A047F175C69897914,
};
extern void NavMeshHit_get_position_m09E8FF6DEF5BFA3F30B3C4BCA4642442FF1BCBF1_AdjustorThunk (void);
static Il2CppTokenAdjustorThunkPair s_adjustorThunks[1] = 
{
	{ 0x06000005, NavMeshHit_get_position_m09E8FF6DEF5BFA3F30B3C4BCA4642442FF1BCBF1_AdjustorThunk },
};
static const int32_t s_InvokerIndices[10] = 
{
	4251,
	7136,
	5881,
	3999,
	7373,
	13233,
	8567,
	8518,
	3270,
	7383,
};
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_UnityEngine_AIModule_CodeGenModule;
const Il2CppCodeGenModule g_UnityEngine_AIModule_CodeGenModule = 
{
	"UnityEngine.AIModule.dll",
	10,
	s_methodPointers,
	1,
	s_adjustorThunks,
	s_InvokerIndices,
	0,
	NULL,
	0,
	NULL,
	0,
	NULL,
	NULL,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
