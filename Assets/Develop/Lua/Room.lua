module("Room", package.seeall)

FrameType =
{
	Fill = 0,
	Key = 1,
};

function Ready()
    Socket:OnPushEvent("onTick", function(msg)
        SocketQueue:Push(function()
        	print(msg);
	        local tbMsg = json.decode(msg);

	        local frameID = nil;
	        local frameType = nil;
	        local clicks = {};

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
		            local pointX = cmd.extra.x;
		            local pointY = cmd.extra.y;
		            local clickFrameID = cmd.extra.fid;

	        		if (frameType == nil) then
	        			frameType = FrameType.Key;
	        		end
	        		clicks[i] = Vector2.New(pointX, pointY);
		            --print("Deal move "..pointX.."  "..pointY);
	    		end
	        end
	        print(frameID, frameType, clicks);
	        FrameSyn.Network.Network.OnFrameStep(frameID, frameType, clicks);
        end);
    end);

    local param = 
    {
        rid = ROOM_ID,
        rsize = ROOM_SIZE
    }
	Socket:Request("battle.battleHandler.enterAndReady", param);
end

function SendMove(point, frameID)
	local param = 
    {
    	type = "shoot",
        extra =
        {
            x = point.x,
            y = point.y,
            fid = frameID,
        }
    }
	--local str = string.format('{"x":%d,"y":%d,"fid":%d}', point.x, point.y, frameID);
    --print(str);
	Socket:Request("battle.battleHandler.operate", param);
end