module("TestMove", package.seeall)

function Init()
    Socket:OnPushEvent("onChat", function(msg)
        SocketQueue:Push(function()
            error(msg);
        end);
    end);
end

function SendMove(point)
	local str = string.format('{"x":%d,"y":%d}', point.x, point.y);
    local param = 
    {
        content = str,
        target = '*',
        rid = 'mobage_cn|debug|world|1'
    }
	print(str);
	Socket:Request("chat.chatHandler.send", param);
end