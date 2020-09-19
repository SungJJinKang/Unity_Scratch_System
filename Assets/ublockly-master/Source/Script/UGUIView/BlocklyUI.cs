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


using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UBlockly.UGUI
{
    public static class BlocklyUI
    {
        public static WorkspaceView WorkspaceView;
        public static Canvas UICanvas;

        public static void NewWorkspace()
        {
            if (WorkspaceView != null)
                throw new Exception("BlocklyUI.NewWorkspace- there is already a workspace");
            
            Workspace workspace = new Workspace(new Workspace.WorkspaceOptions());
            WorkspaceView = Object.FindObjectOfType<WorkspaceView>();
            WorkspaceView.BindModel(workspace);

            UICanvas = WorkspaceView.GetComponentInParent<Canvas>();
        }

        public static void DestroyWorkspace()
        {
            if (WorkspaceView == null)
                return;
            
            WorkspaceView.Dispose();
            GameObject.Destroy(WorkspaceView.gameObject);
        }
    }
}
