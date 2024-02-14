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
}
