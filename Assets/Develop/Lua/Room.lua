module("Room", package.seeall)

function Ready()
    Socket:OnPushEvent("onTick", function(msg)
        SocketQueue:Push(function()
	        local tbMsg = json.decode(msg);
	        --print(tbMsg.cmds[1].tick);
	    	FrameSyn.Network.Network.OnFrameStep(tbMsg.cmds[1].tick / 6);
        end);
    end);

    local param = 
    {
        rid = 99,
        rsize = 2
    }
	Socket:Request("battle.battleHandler.enterAndReady", param);
end