module("TestMove", package.seeall)

function Init()
    Socket:OnPushEvent("onChat", function(msg)
        SocketQueue:Push(function()
        	for k, v in pairs(msg) do
        		error(k, v);
        	end
        end);
    end);
end

function SendMove(point)
	local str = string.format('{"x":%d,"y":%d}', point.x, point.y);
	print(str);
	Socket:Request("chat.chatHandler.send", str);
end