using System.Collections.Generic;
using System.Linq;

public sealed class ActorModule
{
    /// <summary>
    /// UID,index
    /// </summary>
    private Dictionary< byte,long> m_PlayerBattleArrayDic;
    private Dictionary<long, ActorLogicData> m_ActorLogicDic;
    public Dictionary<int, ActorData> m_ActorConfigDic;

    public ActorModule()
    {
        this.InitActor();
        Dictionary<byte,long> battleArray = new Dictionary< byte,long>();
        battleArray.Add(1,10000);
        battleArray.Add(2,10001);
        battleArray.Add(3,10002);
        battleArray.Add(6,10003);
        battleArray.Add(7,10004);
        battleArray.Add(8,10005);
        this.SetBattleArray(battleArray);
    }

    #region private methods

    private void InitActor()
    {
        ActorData heroData0 = new ActorData(1000, "白骨精", "白骨精描述", "Actor_1000", "Hbgj26",20);
        ActorData heroData1 = new ActorData(1001, "楚庄王", "楚庄王描述", "Actor_1001", "Hczw30",18);
        ActorData heroData2 = new ActorData(1002, "貂蝉", "貂蝉描述", "Actor_1002", "Hdc16", 16);
        ActorData heroData3 = new ActorData(1003, "妲己", "妲己描述", "Actor_1003", "Hdj42", 14);
        ActorData heroData4 = new ActorData(1004, "典韦", "典韦描述", "Actor_1004", "Hdw36", 12);
        ActorData heroData5 = new ActorData(1005, "郭嘉", "郭嘉描述", "Actor_1005", "Hgj13", 10);
        ActorData heroData6 = new ActorData(1006, "勾践", "勾践描述", "Actor_1006", "Hgj33", 1);
        ActorData heroData7 = new ActorData(1007, "甘宁", "甘宁描述", "Actor_1007", "Hgn18", 1);
        ActorData heroData8 = new ActorData(1008, "阖闾", "阖闾描述", "Actor_1008", "Hhl31", 1);
        ActorData heroData9 = new ActorData(1009, "荆轲", "荆轲描述", "Actor_1009", "Hjk24", 1);
        ActorData heroData10 = new ActorData(1010, "晋文公", "晋文公描述", "Actor_1010", "Hjwg34", 1);
        ActorData heroData11 = new ActorData(1011, "鲁班", "鲁班描述", "Actor_1011", "Hlb02", 1);
        ActorData heroData12 = new ActorData(1012, "吕布", "吕布描述", "Actor_1012", "Hlb08", 1);
        ActorData heroData13 = new ActorData(1013, "马超", "马超描述", "Actor_1013", "Hmc27", 1);
        ActorData heroData14 = new ActorData(1014, "孟获", "孟获描述", "Actor_1014", "Hmh39", 1);
        ActorData heroData15 = new ActorData(1015, "马良", "马良描述", "Actor_1015", "Hml07", 1);
        ActorData heroData16 = new ActorData(1016, "齐桓公", "齐桓公描述", "Actor_1016", "Hqhg32", 1);
        ActorData heroData17 = new ActorData(1017, "秦始皇", "秦始皇描述", "Actor_1017", "Hqsh19", 1);
        ActorData heroData18 = new ActorData(1018, "孙武", "孙武描述", "Actor_1018", "Hsw40", 1);
        ActorData heroData19 = new ActorData(1019, "杨戬", "杨戬描述", "Actor_1019", "Hyj37", 1);
        ActorData heroData20 = new ActorData(1020, "张辽", "张辽描述", "Actor_1020", "Hzl20", 1);
        ActorData heroData21 = new ActorData(1021, "周瑜", "周瑜描述", "Actor_1021", "Hzy11", 1);

        ActorData monsterData0 = new ActorData(2000, "大强盗", "大强盗描述", "Actor_2000", "G1qd", 19);
        ActorData monsterData1 = new ActorData(2001, "狼", "狼描述", "Actor_2001", "G2L", 17);
        ActorData monsterData2 = new ActorData(2002, "骷髅弓兵", "骷髅弓兵描述", "Actor_2002", "G3kl", 15);
        ActorData monsterData3 = new ActorData(2003, "骷髅枪兵", "骷髅枪兵描述", "Actor_2003", "G4kl", 13);
        ActorData monsterData4 = new ActorData(2004, "僵尸", "僵尸描述", "Actor_2004", "G6js", 11);
        ActorData monsterData5 = new ActorData(2005, "刀兵", "刀兵描述", "Actor_2005", "G7db", 9);
        ActorData monsterData6 = new ActorData(2006, "弓兵", "弓兵描述", "Actor_2006", "G8gj", 1);
        ActorData monsterData7 = new ActorData(2007, "戟兵", "戟兵描述", "Actor_2007", "G9cm", 1);
        ActorData monsterData8 = new ActorData(2008, "小强盗", "小强盗描述", "Actor_2008", "G10qd", 1);
        ActorData monsterData9 = new ActorData(2009, "山贼", "山贼描述", "Actor_2009", "G11sz", 1);
        ActorData monsterData10 = new ActorData(2010, "章鱼", "章鱼公描述", "Actor_2010", "G12zy", 1);
        ActorData monsterData11 = new ActorData(2011, "草泥马", "草泥马描述", "Actor_2011", "G13cnm", 1);
        ActorData monsterData12 = new ActorData(2012, "女兵", "女兵描述", "Actor_2012", "G15nb", 1);
        ActorData monsterData13 = new ActorData(2013, "雅买蝶", "雅买蝶描述", "Actor_2013", "G16ymd", 1);
        ActorData monsterData14 = new ActorData(2014, "小女王", "小女王描述", "Actor_2014", "G17nw", 1);
        ActorData monsterData15 = new ActorData(2015, "大女王", "大女王描述", "Actor_2015", "G18nw", 1);
        ActorData monsterData16 = new ActorData(2016, "策士", "策士描述", "Actor_2016", "G21cs", 1);
        ActorData monsterData17 = new ActorData(2017, "盾兵", "盾兵描述", "Actor_2017", "G23dw", 1);

        this.m_ActorConfigDic = new Dictionary<int, ActorData>();
        this.m_ActorConfigDic.Add(1000, heroData0);
        this.m_ActorConfigDic.Add(1001, heroData1);
        this.m_ActorConfigDic.Add(1002, heroData2);
        this.m_ActorConfigDic.Add(1003, heroData3);
        this.m_ActorConfigDic.Add(1004, heroData4);
        this.m_ActorConfigDic.Add(1005, heroData5);
        this.m_ActorConfigDic.Add(1006, heroData6);
        this.m_ActorConfigDic.Add(1007, heroData7);
        this.m_ActorConfigDic.Add(1008, heroData8);
        this.m_ActorConfigDic.Add(1009, heroData9);
        this.m_ActorConfigDic.Add(1010, heroData10);
        this.m_ActorConfigDic.Add(1011, heroData11);
        this.m_ActorConfigDic.Add(1012, heroData12);
        this.m_ActorConfigDic.Add(1013, heroData13);
        this.m_ActorConfigDic.Add(1014, heroData14);
        this.m_ActorConfigDic.Add(1015, heroData15);
        this.m_ActorConfigDic.Add(1016, heroData16);
        this.m_ActorConfigDic.Add(1017, heroData17);
        this.m_ActorConfigDic.Add(1018, heroData18);
        this.m_ActorConfigDic.Add(1019, heroData19);
        this.m_ActorConfigDic.Add(1020, heroData20);
        this.m_ActorConfigDic.Add(1021, heroData21);

        this.m_ActorConfigDic.Add(2000, monsterData0);
        this.m_ActorConfigDic.Add(2001, monsterData1);
        this.m_ActorConfigDic.Add(2002, monsterData2);
        this.m_ActorConfigDic.Add(2003, monsterData3);
        this.m_ActorConfigDic.Add(2004, monsterData4);
        this.m_ActorConfigDic.Add(2005, monsterData5);
        this.m_ActorConfigDic.Add(2006, monsterData6);
        this.m_ActorConfigDic.Add(2007, monsterData7);
        this.m_ActorConfigDic.Add(2008, monsterData8);
        this.m_ActorConfigDic.Add(2009, monsterData9);
        this.m_ActorConfigDic.Add(2010, monsterData10);
        this.m_ActorConfigDic.Add(2011, monsterData11);
        this.m_ActorConfigDic.Add(2012, monsterData12);
        this.m_ActorConfigDic.Add(2013, monsterData13);
        this.m_ActorConfigDic.Add(2014, monsterData14);
        this.m_ActorConfigDic.Add(2015, monsterData15);
        this.m_ActorConfigDic.Add(2016, monsterData16);
        this.m_ActorConfigDic.Add(2017, monsterData17);

        ActorLogicData heroLogicData0  = new ActorLogicData(10000 , heroData0);
        ActorLogicData heroLogicData1  = new ActorLogicData(10001 , heroData1);
        ActorLogicData heroLogicData2  = new ActorLogicData(10002 , heroData2);
        ActorLogicData heroLogicData3  = new ActorLogicData(10003 , heroData3);
        ActorLogicData heroLogicData4  = new ActorLogicData(10004 , heroData4);
        ActorLogicData heroLogicData5  = new ActorLogicData(10005 , heroData5);
        ActorLogicData heroLogicData6  = new ActorLogicData(10006 , heroData6);
        ActorLogicData heroLogicData7  = new ActorLogicData(10007 , heroData7);
        ActorLogicData heroLogicData8  = new ActorLogicData(10008 , heroData8);
        ActorLogicData heroLogicData9  = new ActorLogicData(10009 , heroData9);
        ActorLogicData heroLogicData10 = new ActorLogicData(10010, heroData10);
        ActorLogicData heroLogicData11 = new ActorLogicData(10011, heroData11);
        ActorLogicData heroLogicData12 = new ActorLogicData(10012, heroData12);
        ActorLogicData heroLogicData13 = new ActorLogicData(10013, heroData13);
        ActorLogicData heroLogicData14 = new ActorLogicData(10014, heroData14);
        ActorLogicData heroLogicData15 = new ActorLogicData(10015, heroData15);
        ActorLogicData heroLogicData16 = new ActorLogicData(10016, heroData16);
        ActorLogicData heroLogicData17 = new ActorLogicData(10017, heroData17);
        ActorLogicData heroLogicData18 = new ActorLogicData(10018, heroData18);
        ActorLogicData heroLogicData19 = new ActorLogicData(10019, heroData19);
        ActorLogicData heroLogicData20 = new ActorLogicData(10020, heroData20);
        ActorLogicData heroLogicData21 = new ActorLogicData(10021, heroData21);

        this.m_ActorLogicDic = new Dictionary<long, ActorLogicData>();
        this.m_ActorLogicDic.Add(10000, heroLogicData0  );
        this.m_ActorLogicDic.Add(10001, heroLogicData1  );
        this.m_ActorLogicDic.Add(10002, heroLogicData2  );
        this.m_ActorLogicDic.Add(10003, heroLogicData3  );
        this.m_ActorLogicDic.Add(10004, heroLogicData4  );
        this.m_ActorLogicDic.Add(10005, heroLogicData5  );
        this.m_ActorLogicDic.Add(10006, heroLogicData6  );
        this.m_ActorLogicDic.Add(10007, heroLogicData7  );
        this.m_ActorLogicDic.Add(10008, heroLogicData8  );
        this.m_ActorLogicDic.Add(10009, heroLogicData9  );
        this.m_ActorLogicDic.Add(10010, heroLogicData10 );
        this.m_ActorLogicDic.Add(10011, heroLogicData11 );
        this.m_ActorLogicDic.Add(10012, heroLogicData12 );
        this.m_ActorLogicDic.Add(10013, heroLogicData13 );
        this.m_ActorLogicDic.Add(10014, heroLogicData14 );
        this.m_ActorLogicDic.Add(10015, heroLogicData15 );
        this.m_ActorLogicDic.Add(10016, heroLogicData16 );
        this.m_ActorLogicDic.Add(10017, heroLogicData17 );
        this.m_ActorLogicDic.Add(10018, heroLogicData18 );
        this.m_ActorLogicDic.Add(10019, heroLogicData19 );
        this.m_ActorLogicDic.Add(10020, heroLogicData20 );
        this.m_ActorLogicDic.Add(10021, heroLogicData21);

    }

    #endregion

    #region public methods

    public Dictionary<byte,long> GetBattleArray()
    {
        return this.m_PlayerBattleArrayDic;
    }

    public void SetBattleArray(Dictionary< byte,long> battleArray)
    {
        this.m_PlayerBattleArrayDic = battleArray;
    }

    public ActorLogicData GetActorLogicDataByUID(long uid)
    {
        return this.m_ActorLogicDic.ContainsKey(uid) ? this.m_ActorLogicDic[uid] : null;
    }

    public ActorData GetActorDataByID(int id)
    {
        return this.m_ActorConfigDic.ContainsKey(id) ? this.m_ActorConfigDic[id] : null;
    }

    #endregion
}
