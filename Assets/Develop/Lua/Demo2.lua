module("Demo2", package.seeall)

local btnReady = nil;

function Init(battleView)
	btnReady = battleView.transform:Find("ReadyButton").gameObject;
	Utility.AddClickEvent(btnReady, function()
		btnReady:SetActive(false);
		Socket:Request("battle.battleHandler.ready", nil);
	end);
end

function Match()
    Socket:OnPushEvent("onTick", function(msg)
        SocketQueue:Push(function()
        	print(msg);
	        local tbMsg = json.decode(msg);

	        local frameID = nil;
	        local frameType = nil;
	        local fids = {};
	        local hs = {};
	        local vs = {};
	        local jumps = {};
	        local pressTs = {};

	        for i = 1,  #tbMsg.cmds do
	        	local cmd = tbMsg.cmds[i];
	        	--[[print('------------------'..i..'------------------');
	        	for k, v in pairs(cmd) do
	        		if (type(v) == "table") then
		        		print(k, next(v));
		        	else
		        		print(k, v);
		        	end
	        	end]]

	        	if (cmd.type == 'sync' or cmd.type == 'start') then
	        		frameID = cmd.tick / 6;
	        		if (frameType == nil) then
		        		frameType = FrameType.Fill;
		        	end
	    		elseif (cmd.type == 'shoot') then
		            local h = cmd.extra.h;
		            local v = cmd.extra.v;
		            local jump = cmd.extra.jump;
		            local pressT = cmd.extra.pressT;
		            local clickFrameID = cmd.extra.fid;

	        		if (frameType == nil) then
	        			frameType = FrameType.Key;
	        		end
	        		hs[i] = h;
	        		vs[i] = v;
	        		jumps[i] = jump;
	        		pressTs[i] = pressT;
			        fids[i] = clickFrameID;
	    		end
	        end
	        FrameSyn.Network.Network.OnFrameStep(frameID, frameType, fids, hs, vs, jumps, pressTs);
	        --FrameSyn.Network.Network.OnFrameStep(frameID, frameType, fids, nil, nil, nil, pressTs);
        end);
    end);

    Socket:OnPushEvent('onMatch', function(s)
        SocketQueue:Push(function()
        	print("handle 'onMatch' msg = '"..s.."'");
        	btnReady:SetActive(true);
        end);
    end);
    Socket:Request("battle.battleHandler.match", nil);
end

function Control(inh, inv, injump, inpressT, frameID)
	local param = 
    {
    	type = "shoot",
        extra =
        {
        	h = inh,
        	v = inv,
        	jump = injump,
        	pressT = inpressT,
            fid = frameID,
        }
    }
	Socket:Request("battle.battleHandler.operate", param);
end

--[[function Control(inpressT, frameID)
	local param = 
    {
    	type = "shoot",
        extra =
        {
        	pressT = inpressT,
            fid = frameID,
        }
    }
	Socket:Request("battle.battleHandler.operate", param);
end]]