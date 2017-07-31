module("Room", package.seeall)

function Ready()
    Socket:OnPushEvent("onTick", function(msg)
        local tbMsg = json.decode(msg);
        print("onTick")
    	FrameSyn.Network.Network.OnFrameStep(tbMsg.tick / 6);
    end);

    local param = 
    {
        rid = 99,
        rsize = 2
    }
	Socket:Request("battle.battleHandler.enterAndReady", param);
end