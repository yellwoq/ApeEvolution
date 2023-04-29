using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-13
    //¹ºÂò¿¨°üÃüÁî
    //**********
    public class BuyCardCommand : AbstractCommand
    {
        private Type mBuyPackageType;
        public BuyCardCommand(Type buyPackageType)
        {
            mBuyPackageType = buyPackageType;
        }
        protected override void OnExecute()
        {
            this.GetSystem<IStoreSystem>().BuyCardPackage(mBuyPackageType);
        }
    }
}
