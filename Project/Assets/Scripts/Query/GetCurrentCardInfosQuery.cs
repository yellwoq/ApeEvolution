using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QFramework;

namespace ApeEvolution
{
    public class CardInfoList
    {
        public List<CardInfo> mCardInfo;
        public CardInfoList(List<CardInfo> mCardInfo)
        {
            this.mCardInfo = mCardInfo;
        }
    }
    /// <summary>
    /// Author:HUANG
    /// Time:2023-2-12
    /// 查询当前卡包信息命令
    /// </summary>
    public class GetCurrentCardInfosQuery : AbstractQuery<CardInfoList>
    {
        private CardPackageType mCardPackageType;
        public GetCurrentCardInfosQuery(CardPackageType cardPackageType)
        {
            this.mCardPackageType = cardPackageType;
        }
        protected override CardInfoList OnDo()
        {
            List<CardInfo> mCardInfo=this.GetSystem<ICardPackageSystem>().GetPackageInfos(mCardPackageType);
            return new CardInfoList(mCardInfo);
        }
    }
}
