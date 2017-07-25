module("TestMove", package.seeall)

function Init()
    Socket:OnPushEvent("onChat", function(msg)
        SocketQueue:Push(function()
            local tbMsg = json.decode(msg);
            local tbContent = json.decode(tbMsg.content);
            local frameID = tbContent.fid;
            print("Deal onChat");
            FrameSyn.Network.Network.OnOperationClick(frameID, tbContent.x, tbContent.y);
        end);
    end);
end

function SendMove(point, frameID)
	local str = string.format('{"x":%d,"y":%d,"fid":%d}', point.x, point.y, frameID);
    local param = 
    {
        content = str,
        target = '*',
        rid = 'mobage_cn|debug|world|1'
    }
	print(str);
	Socket:Request("chat.chatHandler.send", param);
end