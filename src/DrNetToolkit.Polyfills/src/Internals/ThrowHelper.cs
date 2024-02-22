// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace DrNetToolkit.Polyfills.Internals;

[StackTraceHidden]
internal static class ThrowHelper
{
    [DoesNotReturn]
    internal static void ThrowArgumentException_BadComparer(object? comparer)
    {
        throw new ArgumentException(string.Format(SR.Arg_BogusIComparer, comparer));
    }

    [DoesNotReturn]
    internal static void ThrowInvalidOperationException(ExceptionResource resource, Exception e)
    {
        throw new InvalidOperationException(GetResourceString(resource), e);
    }


    [DoesNotReturn]
    internal static void ThrowArgumentNullException(ExceptionArgument argument)
    {
        throw new ArgumentNullException(GetArgumentName(argument));
    }

    [DoesNotReturn]
    internal static void ThrowArgumentOutOfRangeException()
    {
        throw new ArgumentOutOfRangeException();
    }


    [DoesNotReturn]
    internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
    {
        throw new ArgumentOutOfRangeException(GetArgumentName(argument));
    }

    [DoesNotReturn]
    internal static void ThrowArrayTypeMismatchException()
    {
        throw new ArrayTypeMismatchException();
    }

    [DoesNotReturn]
    internal static void ThrowInvalidTypeWithPointersNotSupported(Type targetType)
    {
        throw new ArgumentException(string.Format(SR.Argument_InvalidTypeWithPointersNotSupported, targetType));
    }


    private static string GetArgumentName(ExceptionArgument argument)
    {
        switch (argument)
        {
            //case ExceptionArgument.obj:
            //    return "obj";
            //case ExceptionArgument.dictionary:
            //    return "dictionary";
            case ExceptionArgument.array:
                return "array";
            //case ExceptionArgument.info:
            //    return "info";
            //case ExceptionArgument.key:
            //    return "key";
            case ExceptionArgument.text:
                return "text";
            //case ExceptionArgument.values:
            //    return "values";
            //case ExceptionArgument.value:
            //    return "value";
            case ExceptionArgument.startIndex:
                return "startIndex";
            //case ExceptionArgument.task:
            //    return "task";
            //case ExceptionArgument.bytes:
            //    return "bytes";
            //case ExceptionArgument.byteIndex:
            //    return "byteIndex";
            //case ExceptionArgument.byteCount:
            //    return "byteCount";
            //case ExceptionArgument.ch:
            //    return "ch";
            //case ExceptionArgument.chars:
            //    return "chars";
            //case ExceptionArgument.charIndex:
            //    return "charIndex";
            //case ExceptionArgument.charCount:
            //    return "charCount";
            //case ExceptionArgument.s:
            //    return "s";
            //case ExceptionArgument.input:
            //    return "input";
            //case ExceptionArgument.ownedMemory:
            //    return "ownedMemory";
            //case ExceptionArgument.list:
            //    return "list";
            //case ExceptionArgument.index:
            //    return "index";
            //case ExceptionArgument.capacity:
            //    return "capacity";
            //case ExceptionArgument.collection:
            //    return "collection";
            //case ExceptionArgument.item:
            //    return "item";
            //case ExceptionArgument.converter:
            //    return "converter";
            //case ExceptionArgument.match:
            //    return "match";
            //case ExceptionArgument.count:
            //    return "count";
            //case ExceptionArgument.action:
            //    return "action";
            //case ExceptionArgument.comparison:
            //    return "comparison";
            //case ExceptionArgument.exceptions:
            //    return "exceptions";
            //case ExceptionArgument.exception:
            //    return "exception";
            //case ExceptionArgument.pointer:
            //    return "pointer";
            //case ExceptionArgument.start:
            //    return "start";
            //case ExceptionArgument.format:
            //    return "format";
            //case ExceptionArgument.formats:
            //    return "formats";
            //case ExceptionArgument.culture:
            //    return "culture";
            //case ExceptionArgument.comparer:
            //    return "comparer";
            //case ExceptionArgument.comparable:
            //    return "comparable";
            //case ExceptionArgument.source:
            //    return "source";
            case ExceptionArgument.length:
                return "length";
            //case ExceptionArgument.comparisonType:
            //    return "comparisonType";
            //case ExceptionArgument.manager:
            //    return "manager";
            //case ExceptionArgument.sourceBytesToCopy:
            //    return "sourceBytesToCopy";
            //case ExceptionArgument.callBack:
            //    return "callBack";
            //case ExceptionArgument.creationOptions:
            //    return "creationOptions";
            //case ExceptionArgument.function:
            //    return "function";
            //case ExceptionArgument.scheduler:
            //    return "scheduler";
            //case ExceptionArgument.continuation:
            //    return "continuation";
            //case ExceptionArgument.continuationAction:
            //    return "continuationAction";
            //case ExceptionArgument.continuationFunction:
            //    return "continuationFunction";
            //case ExceptionArgument.tasks:
            //    return "tasks";
            //case ExceptionArgument.asyncResult:
            //    return "asyncResult";
            //case ExceptionArgument.beginMethod:
            //    return "beginMethod";
            //case ExceptionArgument.endMethod:
            //    return "endMethod";
            //case ExceptionArgument.endFunction:
            //    return "endFunction";
            //case ExceptionArgument.cancellationToken:
            //    return "cancellationToken";
            //case ExceptionArgument.continuationOptions:
            //    return "continuationOptions";
            //case ExceptionArgument.delay:
            //    return "delay";
            //case ExceptionArgument.millisecondsDelay:
            //    return "millisecondsDelay";
            //case ExceptionArgument.millisecondsTimeout:
            //    return "millisecondsTimeout";
            //case ExceptionArgument.stateMachine:
            //    return "stateMachine";
            //case ExceptionArgument.timeout:
            //    return "timeout";
            //case ExceptionArgument.type:
            //    return "type";
            //case ExceptionArgument.sourceIndex:
            //    return "sourceIndex";
            //case ExceptionArgument.sourceArray:
            //    return "sourceArray";
            //case ExceptionArgument.destinationIndex:
            //    return "destinationIndex";
            //case ExceptionArgument.destinationArray:
            //    return "destinationArray";
            //case ExceptionArgument.pHandle:
            //    return "pHandle";
            //case ExceptionArgument.handle:
            //    return "handle";
            //case ExceptionArgument.other:
            //    return "other";
            //case ExceptionArgument.newSize:
            //    return "newSize";
            //case ExceptionArgument.lengths:
            //    return "lengths";
            //case ExceptionArgument.len:
            //    return "len";
            //case ExceptionArgument.keys:
            //    return "keys";
            //case ExceptionArgument.indices:
            //    return "indices";
            //case ExceptionArgument.index1:
            //    return "index1";
            //case ExceptionArgument.index2:
            //    return "index2";
            //case ExceptionArgument.index3:
            //    return "index3";
            //case ExceptionArgument.endIndex:
            //    return "endIndex";
            //case ExceptionArgument.elementType:
            //    return "elementType";
            //case ExceptionArgument.arrayIndex:
            //    return "arrayIndex";
            //case ExceptionArgument.year:
            //    return "year";
            //case ExceptionArgument.codePoint:
            //    return "codePoint";
            //case ExceptionArgument.str:
            //    return "str";
            //case ExceptionArgument.options:
            //    return "options";
            //case ExceptionArgument.prefix:
            //    return "prefix";
            //case ExceptionArgument.suffix:
            //    return "suffix";
            //case ExceptionArgument.buffer:
            //    return "buffer";
            //case ExceptionArgument.buffers:
            //    return "buffers";
            //case ExceptionArgument.offset:
            //    return "offset";
            //case ExceptionArgument.stream:
            //    return "stream";
            //case ExceptionArgument.anyOf:
            //    return "anyOf";
            //case ExceptionArgument.overlapped:
            //    return "overlapped";
            //case ExceptionArgument.minimumBytes:
            //    return "minimumBytes";
            //case ExceptionArgument.arrayType:
            //    return "arrayType";
            //case ExceptionArgument.divisor:
            //    return "divisor";
            //case ExceptionArgument.factor:
            //    return "factor";
            default:
                Debug.Assert(false, "The enum value is not defined, please check the ExceptionArgument Enum.");
                return "";
        }
    }

    private static string GetResourceString(ExceptionResource resource)
    {
        switch (resource)
        {
            //case ExceptionResource.ArgumentOutOfRange_IndexMustBeLessOrEqual:
            //    return SR.ArgumentOutOfRange_IndexMustBeLessOrEqual;
            //case ExceptionResource.ArgumentOutOfRange_IndexMustBeLess:
            //    return SR.ArgumentOutOfRange_IndexMustBeLess;
            //case ExceptionResource.ArgumentOutOfRange_IndexCount:
            //    return SR.ArgumentOutOfRange_IndexCount;
            //case ExceptionResource.ArgumentOutOfRange_IndexCountBuffer:
            //    return SR.ArgumentOutOfRange_IndexCountBuffer;
            //case ExceptionResource.ArgumentOutOfRange_Count:
            //    return SR.ArgumentOutOfRange_Count;
            //case ExceptionResource.ArgumentOutOfRange_Year:
            //    return SR.ArgumentOutOfRange_Year;
            //case ExceptionResource.Arg_ArrayPlusOffTooSmall:
            //    return SR.Arg_ArrayPlusOffTooSmall;
            //case ExceptionResource.Arg_ByteArrayTooSmallForValue:
            //    return SR.Arg_ByteArrayTooSmallForValue;
            //case ExceptionResource.NotSupported_ReadOnlyCollection:
            //    return SR.NotSupported_ReadOnlyCollection;
            //case ExceptionResource.Arg_RankMultiDimNotSupported:
            //    return SR.Arg_RankMultiDimNotSupported;
            //case ExceptionResource.Arg_NonZeroLowerBound:
            //    return SR.Arg_NonZeroLowerBound;
            //case ExceptionResource.ArgumentOutOfRange_GetCharCountOverflow:
            //    return SR.ArgumentOutOfRange_GetCharCountOverflow;
            //case ExceptionResource.ArgumentOutOfRange_ListInsert:
            //    return SR.ArgumentOutOfRange_ListInsert;
            //case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
            //    return SR.ArgumentOutOfRange_NeedNonNegNum;
            //case ExceptionResource.ArgumentOutOfRange_SmallCapacity:
            //    return SR.ArgumentOutOfRange_SmallCapacity;
            //case ExceptionResource.Argument_InvalidOffLen:
            //    return SR.Argument_InvalidOffLen;
            //case ExceptionResource.Argument_CannotExtractScalar:
            //    return SR.Argument_CannotExtractScalar;
            //case ExceptionResource.ArgumentOutOfRange_BiggerThanCollection:
            //    return SR.ArgumentOutOfRange_BiggerThanCollection;
            //case ExceptionResource.Serialization_MissingKeys:
            //    return SR.Serialization_MissingKeys;
            //case ExceptionResource.Serialization_NullKey:
            //    return SR.Serialization_NullKey;
            //case ExceptionResource.NotSupported_KeyCollectionSet:
            //    return SR.NotSupported_KeyCollectionSet;
            //case ExceptionResource.NotSupported_ValueCollectionSet:
            //    return SR.NotSupported_ValueCollectionSet;
            //case ExceptionResource.InvalidOperation_NullArray:
            //    return SR.InvalidOperation_NullArray;
            //case ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted:
            //    return SR.TaskT_TransitionToFinal_AlreadyCompleted;
            //case ExceptionResource.TaskCompletionSourceT_TrySetException_NullException:
            //    return SR.TaskCompletionSourceT_TrySetException_NullException;
            //case ExceptionResource.TaskCompletionSourceT_TrySetException_NoExceptions:
            //    return SR.TaskCompletionSourceT_TrySetException_NoExceptions;
            //case ExceptionResource.NotSupported_StringComparison:
            //    return SR.NotSupported_StringComparison;
            //case ExceptionResource.ConcurrentCollection_SyncRoot_NotSupported:
            //    return SR.ConcurrentCollection_SyncRoot_NotSupported;
            //case ExceptionResource.Task_MultiTaskContinuation_NullTask:
            //    return SR.Task_MultiTaskContinuation_NullTask;
            //case ExceptionResource.InvalidOperation_WrongAsyncResultOrEndCalledMultiple:
            //    return SR.InvalidOperation_WrongAsyncResultOrEndCalledMultiple;
            //case ExceptionResource.Task_MultiTaskContinuation_EmptyTaskList:
            //    return SR.Task_MultiTaskContinuation_EmptyTaskList;
            //case ExceptionResource.Task_Start_TaskCompleted:
            //    return SR.Task_Start_TaskCompleted;
            //case ExceptionResource.Task_Start_Promise:
            //    return SR.Task_Start_Promise;
            //case ExceptionResource.Task_Start_ContinuationTask:
            //    return SR.Task_Start_ContinuationTask;
            //case ExceptionResource.Task_Start_AlreadyStarted:
            //    return SR.Task_Start_AlreadyStarted;
            //case ExceptionResource.Task_RunSynchronously_Continuation:
            //    return SR.Task_RunSynchronously_Continuation;
            //case ExceptionResource.Task_RunSynchronously_Promise:
            //    return SR.Task_RunSynchronously_Promise;
            //case ExceptionResource.Task_RunSynchronously_TaskCompleted:
            //    return SR.Task_RunSynchronously_TaskCompleted;
            //case ExceptionResource.Task_RunSynchronously_AlreadyStarted:
            //    return SR.Task_RunSynchronously_AlreadyStarted;
            //case ExceptionResource.AsyncMethodBuilder_InstanceNotInitialized:
            //    return SR.AsyncMethodBuilder_InstanceNotInitialized;
            //case ExceptionResource.Task_ContinueWith_ESandLR:
            //    return SR.Task_ContinueWith_ESandLR;
            //case ExceptionResource.Task_ContinueWith_NotOnAnything:
            //    return SR.Task_ContinueWith_NotOnAnything;
            //case ExceptionResource.Task_InvalidTimerTimeSpan:
            //    return SR.Task_InvalidTimerTimeSpan;
            //case ExceptionResource.Task_Delay_InvalidMillisecondsDelay:
            //    return SR.Task_Delay_InvalidMillisecondsDelay;
            //case ExceptionResource.Task_Dispose_NotCompleted:
            //    return SR.Task_Dispose_NotCompleted;
            //case ExceptionResource.Task_ThrowIfDisposed:
            //    return SR.Task_ThrowIfDisposed;
            //case ExceptionResource.Task_WaitMulti_NullTask:
            //    return SR.Task_WaitMulti_NullTask;
            //case ExceptionResource.ArgumentException_OtherNotArrayOfCorrectLength:
            //    return SR.ArgumentException_OtherNotArrayOfCorrectLength;
            //case ExceptionResource.ArgumentNull_Array:
            //    return SR.ArgumentNull_Array;
            //case ExceptionResource.ArgumentNull_SafeHandle:
            //    return SR.ArgumentNull_SafeHandle;
            //case ExceptionResource.ArgumentOutOfRange_EndIndexStartIndex:
            //    return SR.ArgumentOutOfRange_EndIndexStartIndex;
            //case ExceptionResource.ArgumentOutOfRange_Enum:
            //    return SR.ArgumentOutOfRange_Enum;
            //case ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported:
            //    return SR.ArgumentOutOfRange_HugeArrayNotSupported;
            //case ExceptionResource.Argument_AddingDuplicate:
            //    return SR.Argument_AddingDuplicate;
            //case ExceptionResource.Argument_InvalidArgumentForComparison:
            //    return SR.Argument_InvalidArgumentForComparison;
            //case ExceptionResource.Arg_LowerBoundsMustMatch:
            //    return SR.Arg_LowerBoundsMustMatch;
            //case ExceptionResource.Arg_MustBeType:
            //    return SR.Arg_MustBeType;
            //case ExceptionResource.Arg_Need1DArray:
            //    return SR.Arg_Need1DArray;
            //case ExceptionResource.Arg_Need2DArray:
            //    return SR.Arg_Need2DArray;
            //case ExceptionResource.Arg_Need3DArray:
            //    return SR.Arg_Need3DArray;
            //case ExceptionResource.Arg_NeedAtLeast1Rank:
            //    return SR.Arg_NeedAtLeast1Rank;
            //case ExceptionResource.Arg_RankIndices:
            //    return SR.Arg_RankIndices;
            //case ExceptionResource.Arg_RanksAndBounds:
            //    return SR.Arg_RanksAndBounds;
            case ExceptionResource.InvalidOperation_IComparerFailed:
                return SR.InvalidOperation_IComparerFailed;
            //case ExceptionResource.NotSupported_FixedSizeCollection:
            //    return SR.NotSupported_FixedSizeCollection;
            //case ExceptionResource.Rank_MultiDimNotSupported:
            //    return SR.Rank_MultiDimNotSupported;
            //case ExceptionResource.Arg_TypeNotSupported:
            //    return SR.Arg_TypeNotSupported;
            //case ExceptionResource.Argument_SpansMustHaveSameLength:
            //    return SR.Argument_SpansMustHaveSameLength;
            //case ExceptionResource.Argument_InvalidFlag:
            //    return SR.Argument_InvalidFlag;
            //case ExceptionResource.CancellationTokenSource_Disposed:
            //    return SR.CancellationTokenSource_Disposed;
            //case ExceptionResource.Argument_AlignmentMustBePow2:
            //    return SR.Argument_AlignmentMustBePow2;
            //case ExceptionResource.ArgumentOutOfRange_NotGreaterThanBufferLength:
            //    return SR.ArgumentOutOfRange_NotGreaterThanBufferLength;
            //case ExceptionResource.InvalidOperation_SpanOverlappedOperation:
            //    return SR.InvalidOperation_SpanOverlappedOperation;
            //case ExceptionResource.InvalidOperation_TimeProviderNullLocalTimeZone:
            //    return SR.InvalidOperation_TimeProviderNullLocalTimeZone;
            //case ExceptionResource.InvalidOperation_TimeProviderInvalidTimestampFrequency:
            //    return SR.InvalidOperation_TimeProviderInvalidTimestampFrequency;
            //case ExceptionResource.Format_UnexpectedClosingBrace:
            //    return SR.Format_UnexpectedClosingBrace;
            //case ExceptionResource.Format_UnclosedFormatItem:
            //    return SR.Format_UnclosedFormatItem;
            //case ExceptionResource.Format_ExpectedAsciiDigit:
            //    return SR.Format_ExpectedAsciiDigit;
            //case ExceptionResource.Argument_HasToBeArrayClass:
            //    return SR.Argument_HasToBeArrayClass;
            default:
                Debug.Assert(false, "The enum value is not defined, please check the ExceptionResource Enum.");
                return "";
        }
    }
}

//
// The convention for this enum is using the argument name as the enum name
//
internal enum ExceptionArgument
{
    //obj,
    //dictionary,
    array,
    //info,
    //key,
    text,
    //values,
    //value,
    startIndex,
    //task,
    //bytes,
    //byteIndex,
    //byteCount,
    //ch,
    //chars,
    //charIndex,
    //charCount,
    //s,
    //input,
    //ownedMemory,
    //list,
    //index,
    //capacity,
    //collection,
    //item,
    //converter,
    //match,
    //count,
    //action,
    //comparison,
    //exceptions,
    //exception,
    //pointer,
    //start,
    //format,
    //formats,
    //culture,
    //comparer,
    //comparable,
    //source,
    length,
    //comparisonType,
    //manager,
    //sourceBytesToCopy,
    //callBack,
    //creationOptions,
    //function,
    //scheduler,
    //continuation,
    //continuationAction,
    //continuationFunction,
    //tasks,
    //asyncResult,
    //beginMethod,
    //endMethod,
    //endFunction,
    //cancellationToken,
    //continuationOptions,
    //delay,
    //millisecondsDelay,
    //millisecondsTimeout,
    //stateMachine,
    //timeout,
    //type,
    //sourceIndex,
    //sourceArray,
    //destinationIndex,
    //destinationArray,
    //pHandle,
    //handle,
    //other,
    //newSize,
    //lengths,
    //len,
    //keys,
    //indices,
    //index1,
    //index2,
    //index3,
    //endIndex,
    //elementType,
    //arrayIndex,
    //year,
    //codePoint,
    //str,
    //options,
    //prefix,
    //suffix,
    //buffer,
    //buffers,
    //offset,
    //stream,
    //anyOf,
    //overlapped,
    //minimumBytes,
    //arrayType,
    //divisor,
    //factor,
}

//
// The convention for this enum is using the resource name as the enum name
//
internal enum ExceptionResource
{
    //ArgumentOutOfRange_IndexMustBeLessOrEqual,
    //ArgumentOutOfRange_IndexMustBeLess,
    //ArgumentOutOfRange_IndexCount,
    //ArgumentOutOfRange_IndexCountBuffer,
    //ArgumentOutOfRange_Count,
    //ArgumentOutOfRange_Year,
    //Arg_ArrayPlusOffTooSmall,
    //Arg_ByteArrayTooSmallForValue,
    //NotSupported_ReadOnlyCollection,
    //Arg_RankMultiDimNotSupported,
    //Arg_NonZeroLowerBound,
    //ArgumentOutOfRange_GetCharCountOverflow,
    //ArgumentOutOfRange_ListInsert,
    //ArgumentOutOfRange_NeedNonNegNum,
    //ArgumentOutOfRange_NotGreaterThanBufferLength,
    //ArgumentOutOfRange_SmallCapacity,
    //Argument_InvalidOffLen,
    //Argument_CannotExtractScalar,
    //ArgumentOutOfRange_BiggerThanCollection,
    //Serialization_MissingKeys,
    //Serialization_NullKey,
    //NotSupported_KeyCollectionSet,
    //NotSupported_ValueCollectionSet,
    //InvalidOperation_NullArray,
    //TaskT_TransitionToFinal_AlreadyCompleted,
    //TaskCompletionSourceT_TrySetException_NullException,
    //TaskCompletionSourceT_TrySetException_NoExceptions,
    //NotSupported_StringComparison,
    //ConcurrentCollection_SyncRoot_NotSupported,
    //Task_MultiTaskContinuation_NullTask,
    //InvalidOperation_WrongAsyncResultOrEndCalledMultiple,
    //Task_MultiTaskContinuation_EmptyTaskList,
    //Task_Start_TaskCompleted,
    //Task_Start_Promise,
    //Task_Start_ContinuationTask,
    //Task_Start_AlreadyStarted,
    //Task_RunSynchronously_Continuation,
    //Task_RunSynchronously_Promise,
    //Task_RunSynchronously_TaskCompleted,
    //Task_RunSynchronously_AlreadyStarted,
    //AsyncMethodBuilder_InstanceNotInitialized,
    //Task_ContinueWith_ESandLR,
    //Task_ContinueWith_NotOnAnything,
    //Task_InvalidTimerTimeSpan,
    //Task_Delay_InvalidMillisecondsDelay,
    //Task_Dispose_NotCompleted,
    //Task_ThrowIfDisposed,
    //Task_WaitMulti_NullTask,
    //ArgumentException_OtherNotArrayOfCorrectLength,
    //ArgumentNull_Array,
    //ArgumentNull_SafeHandle,
    //ArgumentOutOfRange_EndIndexStartIndex,
    //ArgumentOutOfRange_Enum,
    //ArgumentOutOfRange_HugeArrayNotSupported,
    //Argument_AddingDuplicate,
    //Argument_InvalidArgumentForComparison,
    //Arg_LowerBoundsMustMatch,
    //Arg_MustBeType,
    //Arg_Need1DArray,
    //Arg_Need2DArray,
    //Arg_Need3DArray,
    //Arg_NeedAtLeast1Rank,
    //Arg_RankIndices,
    //Arg_RanksAndBounds,
    InvalidOperation_IComparerFailed,
    //NotSupported_FixedSizeCollection,
    //Rank_MultiDimNotSupported,
    //Arg_TypeNotSupported,
    //Argument_SpansMustHaveSameLength,
    //Argument_InvalidFlag,
    //CancellationTokenSource_Disposed,
    //Argument_AlignmentMustBePow2,
    //InvalidOperation_SpanOverlappedOperation,
    //InvalidOperation_TimeProviderNullLocalTimeZone,
    //InvalidOperation_TimeProviderInvalidTimestampFrequency,
    //Format_UnexpectedClosingBrace,
    //Format_UnclosedFormatItem,
    //Format_ExpectedAsciiDigit,
    //Argument_HasToBeArrayClass,
}
