using System;

namespace FFXIV.Packets
{
    public enum PacketId : UInt32
    {
        WorldC2SConnect = 1,
        WorldS2CAuth = 2,
        Opcode = 3,
        Ping = 7,
        Pong = 8,
        LobbyC2SConnect = 9,
        LobbyS2CAuth = 0xA,
    }
    public enum LobbyOpcode : UInt16
    {
        LobbyS2CError = 0x2,
        LobbyC2SCharList = 0x3,
        LobbyC2SCharConnect = 0x4,
        LobbyC2SAuth = 0x5,
        LobbyS2CAccountList = 0xC,
        LobbyS2CCharList = 0xD,
        LobbyS2CWorldEnter = 0xF,
        LobbyS2CWorldList = 0x15,
        LobbyS2CWorldOpen = 0x16,
        LobbyS2CRetainerList = 0x17,
    }
    public enum ChatOpcode : UInt16
    {
        ChatS2CAuth = 2
    }
    public enum WorldOpcode : UInt16
    {
        WorldPing = 0x0065,
        WorldInit = 0x0066,


        WorldC2SFinishLoadingHandler = 0x0069, // unchanged 5.0

        WorldC2SCFCommenceHandler = 0x006F,


        WorldC2SCFRegisterDuty = 0x0071,
        WorldC2SCFRegisterRoulette = 0x0072,
        WorldC2SPlayTimeHandler = 0x0073, // unchanged 5.0
        WorldC2SLogoutHandler = 0x0074, // unchanged 5.0
        WorldC2SCancelLogout = 0x0075, // updated 5.0

        //WorldC2SCFDutyInfoHandler = 0x0078, // updated 4.2

        //WorldC2SSocialReqSendHandler = 0x00AE, // updated 4.1
        //WorldC2SCreateCrossWorldLS = 0x00AF, // updated 4.3

        WorldC2SChatHandler = 0x00D9, // updated 5.0

        WorldC2SSocialListHandler = 0x00E1, // updated 5.0
        WorldC2SSetSearchInfoHandler = 0x00E4, // updated 5.0
        WorldC2SReqSearchInfoHandler = 0x00E6, // updated 5.0
        WorldC2SReqExamineSearchCommentHandler = 0x00E7, // updated 5.0

        WorldC2SReqRemovePlayerFromBlacklist = 0x00F1, // updated 5.0
        WorldC2SBlackListHandler = 0x00F2, // updated 5.0
        WorldC2SPlayerSearchHandler = 0x00F4, // updated 5.0

        WorldC2SLinkshellListHandler = 0x00FA, // updated 5.0

        //WorldC2SMarketBoardRequestItemListingInfo = 0x0102, // updated 4.5
        //WorldC2SMarketBoardRequestItemListings = 0x0103, // updated 4.5
        //WorldC2SMarketBoardSearch = 0x0107, // updated 4.5

        //WorldC2SReqExamineFcInfo = 0x0113, // updated 4.1

        //WorldC2SFcInfoReqHandler = 0x011A, // updated 4.2

        //WorldC2SReqMarketWishList = 0x012C, // updated 4.3

        //WorldC2SReqJoinNoviceNetwork = 0x0129, // updated 4.2

        //WorldC2SReqCountdownInitiate = 0x0133, // updated 4.5
        //WorldC2SReqCountdownCancel = 0x0134, // updated 4.5
        //WorldC2SClearWaymarks = 0x0135, // updated 4.5

        WorldC2SZoneLineHandler = 0x0139, // updated 5.0
        WorldC2SClientTrigger = 0x013A, // updated 5.0
        WorldC2SDiscoveryHandler = 0x013B, // updated 5.0

        //WorldC2SAddWaymark = 0x013A, // updated 4.5

        WorldC2SSkillHandler = 0x013D, // updated 5.0
        WorldC2SGMCommand1 = 0x013E, // updated 5.0
        WorldC2SGMCommand2 = 0x013F, // updated 5.0
        WorldC2SAoESkillHandler = 0x140, // updated 5.0

        WorldC2SUpdatePosition = 0x0141, // updated 5.0

        WorldC2SInventoryModifyHandler = 0x0148, // updated 5.0

        WorldC2SReqPlaceHousingItem = 0x014B, // updated 5.0
        WorldC2SBuildPresetHandler = 0x014F, // updated 5.0

        WorldC2STalkEventHandler = 0x0151, // updated 5.0
        WorldC2SEmoteEventHandler = 0x0152, // updated 5.0
        WorldC2SWithinRangeEventHandler = 0x0153, // updated 5.0
        WorldC2SOutOfRangeEventHandler = 0x0154, // updated 5.0
        WorldC2SEnterTeriEventHandler = 0x0155, // updated 5.0
        WorldC2SShopEventHandler = 0x0156, // updated 5.0

        WorldC2SReturnEventHandler = 0x015A, // updated 5.0?
        WorldC2STradeReturnEventHandler = 0x015B, // updated 5.0?

        WorldC2STriadMenu = 0x163,
        WorldC2STriadPlayCard = 0x164,
        WorldC2STriadPlay = 0x165,

        //WorldC2SLinkshellEventHandler = 0x016B, // updated 4.5
        //WorldC2SLinkshellEventHandler1 = 0x016C, // updated 4.5

        WorldC2SReqEquipDisplayFlagsChange = 0x0175, // updated 5.0

        WorldC2SLandRenameHandler = 0x0177, // updated 5.0
        WorldC2SHousingUpdateHouseGreeting = 0x0178, // updated 5.0
        WorldC2SHousingUpdateObjectPosition = 0x0179, // updated 5.0

        WorldC2SSetSharedEstateSettings = 0x017B, // updated 5.0

        WorldC2SUpdatePositionInstance = 0x0180, // updated 5.0

        WorldC2SPerformNoteHandler = 0x029B, // updated 4.3

        WorldS2CActorFreeSpawn = 0x0191,
        WorldS2CInitZone = 0x019A,

        WorldS2CEffectResult = 0x0141,
        WorldS2CActorControl142 = 0x0142,
        WorldS2CActorControl143 = 0x0143,
        WorldS2CActorControl144 = 0x0144,
        WorldS2CUpdateHpMpTp = 0x0145,

        WorldS2CChatBanned = 0x006B,
        WorldS2CPlaytime = 0x0100, // updated 5.0
        WorldS2CLogout = 0x0077, // updated 5.0
        WorldS2CCFNotify = 0x0078,
        WorldS2CCFMemberStatus = 0x0079,
        WorldS2CCFDutyInfo = 0x007A,
        WorldS2CCFPlayerInNeed = 0x007F,

        WorldS2CSocialRequestError = 0x00AD,

        //WorldS2CCFRegistered = 0x00B8, // updated 4.1
        //WorldS2CSocialRequestResponse = 0x00BB, // updated 4.1
        // WorldS2CCancelAllianceForming = 0x00C6, // updated 4.2

        WorldS2CLogMessage = 0x00D0,

        WorldS2CChat = 0x0104, // updated 5.0

        //WorldS2CWorldVisitList = 0x00FE, // added 4.5

        WorldS2CSocialList = 0x010D, // updated 5.0

        WorldS2CUpdateSearchInfo = 0x0110, // updated 5.0
        WorldS2CInitSearchInfo = 0x0111, // updated 5.0
                                         //WorldS2CExamineSearchComment = 0x0102, // updated 4.1

        WorldS2CServerNoticeShort = 0x0115, // updated 5.0
        WorldS2CServerNotice = 0x0116, // updated 5.0
        WorldS2CSetOnlineStatus = 0x0117, // updated 5.0

        WorldS2CCountdownInitiate = 0x011E, // updated 5.0
        WorldS2CCountdownCancel = 0x011F, // updated 5.0

        WorldS2CPlayerAddedToBlacklist = 0x0120, // updated 5.0
        WorldS2CPlayerRemovedFromBlacklist = 0x0121, // updated 5.0
        WorldS2CBlackList = 0x0123, // updated 5.0

        WorldS2CLinkshellList = 0x012A, // updated 5.0

        WorldS2CMailDeleteRequest = 0x012B, // updated 5.0

        WorldS2CReqMoogleMailList = 0x0138, // updated 5.0
        WorldS2CReqMoogleMailLetter = 0x0139, // updated 5.0
        WorldS2CMailLetterNotification = 0x013A, // updated 5.0

        //WorldS2CMarketBoardItemListingCount = 0x0125, // updated 4.5
        //WorldS2CMarketBoardItemListing = 0x0126, // updated 4.5
        //WorldS2CMarketBoardItemListingHistory = 0x012A, // updated 4.5
        //WorldS2CMarketBoardSearchResult = 0x0139, // updated 4.5

        //WorldS2CCharaFreeCompanyTag = 0x013B, // updated 4.5
        //WorldS2CFreeCompanyBoardMsg = 0x013C, // updated 4.5
        //WorldS2CFreeCompanyInfo = 0x013D, // updated 4.5
        //WorldS2CExamineFreeCompanyInfo = 0x013E, // updated 4.5

        WorldS2CStatusEffectList = 0x015B, // updated 5.0
        WorldS2CEurekaStatusEffectList = 0x015C, // updated 5.0
        WorldS2CEffect = 0x015E, // updated 5.0
        WorldS2CAoeEffect8 = 0x0161, // updated 5.0
        WorldS2CAoeEffect16 = 0x0162, // updated 5.0
        WorldS2CAoeEffect24 = 0x0163, // updated 5.0
        WorldS2CAoeEffect32 = 0x0164, // updated 5.0
        WorldS2CPersistantEffect = 0x0165, // updated 5.0

        WorldS2CGCAffiliation = 0x016F, // updated 5.0

        WorldS2CPlayerSpawn = 0x017F, // updated 5.0
        WorldS2CNpcSpawn = 0x0180, // updated 5.0
        WorldS2CNpcSpawn2 = 0x0181, // ( Bigger statuseffectlist? ) updated 5.0
        WorldS2CActorMove = 0x0182, // updated 5.0

        WorldS2CActorSetPos = 0x0184, // updated 5.0

        WorldS2CActorCast = 0x0187, // updated 5.0

        WorldS2CPartyList = 0x0188, // updated 5.0
        WorldS2CHateRank = 0x0189, // updated 5.0
        WorldS2CHateList = 0x018A, // updated 5.0
        WorldS2CObjectSpawn = 0x018B, // updated 5.0
        WorldS2CObjectDespawn = 0x018C, // updated 5.0
        WorldS2CUpdateClassInfo = 0x018D, // updated 5.0
        WorldS2CSilentSetClassJob = 0x018E, // updated 5.0 - seems to be the case, not sure if it's actually used for anything
        WorldS2CPlayerSetup = 0x018F, // updated 5.0
        WorldS2CPlayerStats = 0x0190, // updated 5.0
        WorldS2CActorOwner = 0x0192, // updated 5.0
        WorldS2CPlayerStateFlags = 0x0193, // updated 5.0
        WorldS2CPlayerClassInfo = 0x0194, // updated 5.0

        WorldS2CModelEquip = 0x0196, // updated 5.0
        WorldS2CExamine = 0x0197, // updated 5.0
        WorldS2CCharaNameReq = 0x0198, // updated 5.0


        WorldS2CUpdateRetainerItemSalePrice = 0x019F, // updated 5.0

        //WorldS2CSetLevelSync = 0x1186, // not updated for 4.4, not sure what it is anymore

        WorldS2CItemInfo = 0x01A1, // updated 5.0
        WorldS2CContainerInfo = 0x01A2, // updated 5.0
        WorldS2CInventoryTransactionFinish = 0x01A3, // updated 5.0
        WorldS2CInventoryTransaction = 0x01A4, // updated 5.0

        WorldS2CCurrencyCrystalInfo = 0x01A5, // updated 5.0

        WorldS2CInventoryActionAck = 0x01A7, // updated 5.0
        WorldS2CUpdateInventorySlot = 0x01A8, // updated 5.0

        WorldS2CHuntingLogEntry = 0x01B3, // updated 5.0

        WorldS2CEventPlay = 0x01B5, // updated 5.0
        WorldS2CDirectorPlayScene = 0x01B9, // updated 5.0
        WorldS2CEventOpenGilShop = 0x01BC, // updated 5.0

        WorldS2CEventStart = 0x01BE, // updated 5.0
        WorldS2CEventFinish = 0x01BF, // updated 5.0

        WorldS2CEventLinkshell = 0x1169,

        WorldS2CQuestActiveList = 0x01D2, // updated 5.0
        WorldS2CQuestUpdate = 0x01D3, // updated 5.0
        WorldS2CQuestCompleteList = 0x01D4, // updated 5.0

        WorldS2CQuestFinish = 0x01D5, // updated 5.0
        WorldS2CMSQTrackerComplete = 0x01D6, // updated 5.0
                                             //WorldS2CMSQTrackerProgress = 0xF1CD, // updated 4.5 ? this actually looks like the two opcodes have been combined, see #474

        WorldS2CQuestMessage = 0x01DE, // updated 5.0

        WorldS2CQuestTracker = 0x01E3, // updated 5.0

        WorldS2CMount = 0x01F3, // updated 5.0

        WorldS2CDirectorVars = 0x01F5, // updated 5.0
        WorldS2CDirectorPopUp = 0x0200, // updated 5.0 - display dialogue pop-ups in duties and FATEs, for example, Teraflare's countdown

        //WorldS2CCFAvailableContents = 0xF1FD, // updated 4.2

        WorldS2CWeatherChange = 0x0210, // updated 5.0
        WorldS2CPlayerTitleList = 0x0211, // updated 5.0
        WorldS2CDiscovery = 0x0212, // updated 5.0

        WorldS2CEorzeaTimeOffset = 0x0214, // updated 5.0

        WorldS2CEquipDisplayFlags = 0x0220, // updated 5.0


        WorldS2CLandSetInitialize = 0x0234, // updated 5.0
        WorldS2CLandUpdate = 0x0235, // updated 5.0
        WorldS2CYardObjectSpawn = 0x0236, // updated 5.0
        WorldS2CHousingIndoorInitialize = 0x0237, // updated 5.0
        WorldS2CLandPriceUpdate = 0x0238, // updated 5.0
        WorldS2CLandInfoSign = 0x0239, // updated 5.0
        WorldS2CLandRename = 0x023A, // updated 5.0
        WorldS2CHousingEstateGreeting = 0x023B, // updated 5.0
        WorldS2CHousingUpdateLandFlagsSlot = 0x023C, // updated 5.0
        WorldS2CHousingLandFlags = 0x023D, // updated 5.0
        WorldS2CHousingShowEstateGuestAccess = 0x023E, // updated 5.0

        WorldS2CHousingObjectInitialize = 0x0240, // updated 5.0
        WorldS2CHousingInternalObjectSpawn = 0x241, // updated 5.0

        WorldS2CHousingWardInfo = 0x0243, // updated 5.0
        WorldS2CHousingObjectMove = 0x0244, // updated 5.0

        //WorldS2CSharedEstateSettingsResponse = 0x0245, // updated 4.5

        //WorldS2CLandUpdateHouseName = 0x0257, // updated 4.5

        //WorldS2CLandSetMap = 0x025B, // updated 4.5


        //WorldS2CDuelChallenge = 0x0277, // 4.2; this is responsible for opening the ui
        //WorldS2CPerformNote = 0x0286, // updated 4.3

        WorldS2CPrepareZoning = 0x02A4, // updated 5.0
                                        //WorldS2CActorGauge = 0x0292, // updated 4.3

        WorldS2CIPCTYPE_UNK_320 = 0x025E, // updated 5.0
        WorldS2CIPCTYPE_UNK_322 = 0x0260, // updated 5.0

        WorldS2CMahjongOpenGui = 0x02A4, // only available in mahjong instance
        WorldS2CMahjongNextRound = 0x02BD, // initial hands(baipai), # of riichi(wat), winds, honba, score and stuff
        WorldS2CMahjongPlayerAction = 0x02BE, // tsumo(as in drawing a tile) called chi/pon/kan/riichi
        WorldS2CMahjongEndRoundTsumo = 0x02BF, // called tsumo
        WorldS2CMahjongEndRoundRon = 0x2C0, // called ron or double ron (waiting for action must be flagged from discard packet to call)
        WorldS2CMahjongTileDiscard = 0x02C1, // giri (discarding a tile.) chi(1)/pon(2)/kan(4)/ron(8) flags etc..
        WorldS2CMahjongPlayersInfo = 0x02C2, // actor id, name, rating and stuff..
        WorldS2CMahjongEndRoundDraw = 0x02C5, // self explanatory
        WorldS2CMahjongEndGame = 0x02C6, // finished oorasu(all-last) round; shows a result screen.
    }
}
