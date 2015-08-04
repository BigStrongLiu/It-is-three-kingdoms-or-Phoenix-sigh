public enum UIPanelType : byte
{
    None,

    #region Login

    /// <summary>
    /// 登录面板
    /// </summary>
    LoginPanel,

    /// <summary>
    /// 选择服务器面板
    /// </summary>
    SwitchServerPanel,

    /// <summary>
    /// 创建角色面板
    /// </summary>
    CreateRolePanel,

    /// <summary>
    /// 选择测试服务器
    /// </summary>
    SwitchTestServerPanel,

    /// <summary>
    /// 开始界面
    /// </summary>
    StartPanel,

    /// <summary>
    /// 注册界面
    /// </summary>
    RegisterPanel,

    /// <summary>
    /// 服务器界面
    /// </summary>
    ServerPanel,

    ///<summary>
    ///登录背景界面
    /// </summary>
    BackgroundPanel,

    ///<summary>
    ///新功能抢先看界面
    /// </summary>
    ShowNewFunctionPanel,

    #endregion

    #region Main

    /// <summary>
    /// 装备附魔
    /// </summary>
    EnchantPanel,

    /// <summary>
    /// 英雄升级面板
    /// </summary>
    UpgradePanel,

    /// <summary>
    /// 灵珠面板
    /// </summary>
    LingZhuPanel,

    /// <summary>
    /// 灵珠召唤面板
    /// </summary>
    LingZhuSummonPanel,

    /// <summary>
    /// 灵珠佩戴面板
    /// </summary>
    LingZhuChangePanel,

    /// <summary>
    /// 灵珠升级面板
    /// </summary>
    LingZhuUpgradePanel,

    /// <summary>
    /// 任务
    /// </summary>
    TaskPanel,

    /// <summary>
    /// 日常任务
    /// </summary>
    DailyTaskPanel,

    /// <summary>
    /// 邮箱面板
    /// </summary>
    MailPanel,

    /// <summary>
    /// 神秘商人
    /// </summary>
    MysteriousBusinessmanPanel,

    /// <summary>
    /// 装备强化面板
    /// </summary>
    EquipmentStrengthenPanel,

    /// <summary>
    /// 新英雄强化面板
    /// </summary>
    EquipmentUpgradePanel,

    /// <summary>
    /// 仓库面板
    /// </summary>
    WarehousePanel,

    /// <summary>
    /// 成就面板
    /// </summary>
    AchievementPanel,

    /// <summary>
    /// 挑战关卡
    /// </summary>
    LevelBossPanel,

    /// <summary>
    /// 竞技场
    /// </summary>
    ArenaPanel,

    /// <summary>
    /// 竞技场排行
    /// </summary>
    ArenaTopPanel,

    /// <summary>
    /// 竞技场商店
    /// </summary>
    ArenaStorePanel,

    /// <summary>
    /// 竞技场战报
    /// </summary>
    ArenaBattleReportPanel,

    /// <summary>
    /// 主公
    /// </summary>
    GeneralPanel,

    /// <summary>
    /// 主公修炼
    /// </summary>
    PracticePanel,

    /// <summary>
    /// 主公天赋
    /// </summary>
    TalentPanel,

    /// <summary>
    /// 扫荡结算
    /// </summary>
    MopupSettlementPanel,

    /// <summary>
    /// 公告面板
    /// </summary>
    AnnouncementPanel,

    /// <summary>
    /// 主面板
    /// </summary>
    MainPanel,

    ///<summary>
    ///阵容面板
    /// </summary>
    BattleArrayPanel,

    ///<summary>
    ///章节面板
    /// </summary>
    StagePanel,

    /// <summary>
    /// 英雄管理面板（英雄之家）
    /// </summary>
    HeroPanel,

    /// <summary>
    /// 商店面板
    /// </summary>
    MallPanel,

    /// <summary>
    /// 抽奖面板
    /// </summary>
    LotteryPanel,

    /// <summary>
    /// 通用商店
    /// 集市、黑市、竞技场商店
    /// </summary>
    CommonShopPanel,

    /// <summary>
    /// 签到
    /// </summary>
    SignPanel,

    /// <summary>
    /// 蚩尤墓
    /// </summary>
    ChiYouMuPanel,

    /// <summary>
    /// 礼包
    /// </summary>
    GiftPanel,
    
    /// <summary>
    /// 出售界面
    /// </summary>
    SellPanel,

    ///<summary>
    ///掉落界面
    /// </summary>
    FallPanel,

    /// <summary>
    /// 玩家
    /// </summary>
    PlayerPanel,

    /// <summary>
    /// 点石成金界面
    /// </summary>
    MidasTouchPanel,

    #region Second Popup

    /// <summary>
    /// 商店购买弹出框
    /// </summary>
    ShopBuyPopupPanel,

    /// <summary>
    /// 抽奖结果
    /// </summary>
    LotteryResultPanel,

    /// <summary>
    /// 玩家阵容
    /// </summary>
    ArenaPlayerBattleArrayPopupPanel,

    /// <summary>
    /// 竞技场商店购买
    /// </summary>
    ArenaStoreBuyPopupPanel,

    /// <summary>
    /// 英雄进化成功
    /// </summary>
    EvolutionResultPanel,

    /// <summary>
    /// 召唤英雄成功
    /// </summary>
    SoulToHeroPopupPanel,

    /// <summary>
    /// 邮件内容
    /// </summary>
    MailContentPopupPanel,

    /// <summary>
    /// 蚩尤墓关卡信息
    /// </summary>
    ChiYouMuLevelInfoPopupPanel,

    /// <summary>
    /// 蚩尤墓关卡奖励
    /// </summary>
    ChiYouMuLevelAwardPopupPanel,

    /// <summary>
    /// 蚩尤墓奖励
    /// </summary>
    ChiYouMuAwardPopupPanel,

    /// <summary>
    /// 更改昵称
    /// </summary>
    ChangeNickNamePopupPanel,

    /// <summary>
    /// 日常任务积分奖励
    /// </summary>
    ScoreAwardPopupPanel,

    #endregion

    #endregion

    #region Fight

    /// <summary>
    /// 战斗面板
    /// </summary>
    CommonFightPanel,

    /// <summary>
    /// 战斗胜利面板
    /// </summary>
    CommonVictoryPanel,

    /// <summary>
    /// 蚩尤墓战斗胜利面板
    /// </summary>
    ChiYouMuVictoryPanel,

    /// <summary>
    /// 战斗失败面板
    /// </summary>
    FailPanel,

    /// <summary>
    /// 战斗设置
    /// </summary>
    FightSettingPanel,

    /// <summary>
    /// 竞技场战斗
    /// </summary>
    ArenaFightPanel,

    /// <summary>
    /// 竞技场胜利
    /// </summary>
    ArenaVictoryPanel,

    /// <summary>
    /// 战斗开始
    /// </summary>
    FightStartPanel,

    /// <summary>
    /// 新手引导战斗面板
    /// </summary>
    GuideFightPanel,

    #endregion

    #region Common

    AlertPopupPanel,

    /// <summary>
    /// 主公升级
    /// </summary>
    GeneralUpgradePopupPanel,

    /// <summary>
    /// 获取金币
    /// </summary>
    GetGoldPopupPanel,

    /// <summary>
    /// 成就达成
    /// </summary>
    AchievementReachedPanel,

    /// <summary>
    /// 获得英雄
    /// </summary>
    GotHeroPopupPanel,

    #endregion

}