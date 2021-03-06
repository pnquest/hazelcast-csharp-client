// Copyright (c) 2008-2017, Hazelcast, Inc. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Hazelcast.Client.Protocol.Util;
using Hazelcast.IO;
using Hazelcast.IO.Serialization;

namespace Hazelcast.Client.Protocol.Codec
{
    internal sealed class TransactionalQueuePollCodec
    {
        public const int ResponseType = 105;
        public const bool Retryable = false;

        public static readonly TransactionalQueueMessageType RequestType =
            TransactionalQueueMessageType.TransactionalQueuePoll;

        public static ResponseParameters DecodeResponse(IClientMessage clientMessage)
        {
            var parameters = new ResponseParameters();
            IData response = null;
            var response_isNull = clientMessage.GetBoolean();
            if (!response_isNull)
            {
                response = clientMessage.GetData();
                parameters.response = response;
            }
            return parameters;
        }

        public static ClientMessage EncodeRequest(string name, string txnId, long threadId, long timeout)
        {
            var requiredDataSize = RequestParameters.CalculateDataSize(name, txnId, threadId, timeout);
            var clientMessage = ClientMessage.CreateForEncode(requiredDataSize);
            clientMessage.SetMessageType((int) RequestType);
            clientMessage.SetRetryable(Retryable);
            clientMessage.Set(name);
            clientMessage.Set(txnId);
            clientMessage.Set(threadId);
            clientMessage.Set(timeout);
            clientMessage.UpdateFrameLength();
            return clientMessage;
        }

        //************************ REQUEST *************************//

        public class RequestParameters
        {
            public static readonly TransactionalQueueMessageType TYPE = RequestType;
            public string name;
            public long threadId;
            public long timeout;
            public string txnId;

            public static int CalculateDataSize(string name, string txnId, long threadId, long timeout)
            {
                var dataSize = ClientMessage.HeaderSize;
                dataSize += ParameterUtil.CalculateDataSize(name);
                dataSize += ParameterUtil.CalculateDataSize(txnId);
                dataSize += Bits.LongSizeInBytes;
                dataSize += Bits.LongSizeInBytes;
                return dataSize;
            }
        }

        //************************ RESPONSE *************************//


        public class ResponseParameters
        {
            public IData response;
        }
    }
}