﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.DocAsCode.EntityModel.Plugins
{
    using System.Collections.Generic;
    using System.Composition;

    using Microsoft.DocAsCode.EntityModel.ViewModels;
    using Microsoft.DocAsCode.Plugins;
    using System.Linq;
    [Export(nameof(ManagedReferenceDocumentProcessor), typeof(IDocumentBuildStep))]
    public class ApplyOverwriteDocumentForMref : ApplyOverwriteDocument<ItemViewModel>
    {
        public override string Name => nameof(ApplyOverwriteDocumentForMref);

        public override int BuildOrder => 0x10;

        protected override IEnumerable<ItemViewModel> GetItemsFromOverwriteDocument(FileModel fileModel, string uid, IHostService host)
        {
            return OverwriteDocumentReader.Transform<ItemViewModel>(
                fileModel,
                uid,
                s => BuildManagedReferenceDocument.BuildItem(host, s, fileModel, content => content != null && content.Trim() == Constants.ContentPlaceholder));
        }

        protected override IEnumerable<ItemViewModel> GetItemsToOverwrite(FileModel model, string uid, IHostService host)
        {
            return ((PageViewModel)model.Content).Items.Where(s => s.Uid == uid);
        }
    }
}
