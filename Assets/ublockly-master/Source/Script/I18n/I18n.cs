﻿/****************************************************************************

Copyright 2016 sophieml1989@gmail.com

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

****************************************************************************/

using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace UBlockly
{
    public class I18n
    {
        public const string EN = "en";
        public const string CN = "cn";

        private static Dictionary<string, string> mMsg = null;
        public static Dictionary<string, string> Msg
        {
            get
            {
                if (mMsg == null)
                    BlockResMgr.Get().LoadI18n(I18n.EN);
                return mMsg;
            }
        }

        public static void AddI18nFile(string text)
        {
            if (mMsg == null)
                mMsg = new Dictionary<string, string>();
            
            JObject jobj = JObject.Parse(text);
            foreach (KeyValuePair<string, JToken> pair in jobj)
            {
                mMsg[pair.Key] = pair.Value.ToString();
            }
        }

        public static void Dispose()
        {
            if (mMsg != null)
                mMsg.Clear();
            mMsg = null;
        }
    }
}
