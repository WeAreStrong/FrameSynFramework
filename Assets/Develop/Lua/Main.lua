require "Common/ktJson"
require "Common/LuaExtension"
SocketQueue = require('SocketQueue')

local mTcpPrefix = 'fifa-dev.mbgadev.cn';
local mTcpPort = 3050;

ROOM_ID = 99;
ROOM_SIZE = 1;

function Main(_uid)
	local socket = require("Socket");
	Socket = socket.new();
    local user = 
    {
        uid = _uid,
        sid = 1
    }

    function connectLogic()
        Socket:Connect(user, function() Socket:Request('connector.entryHandler.enter', user) end);
    end
    Socket:InitClient(mTcpPrefix, mTcpPort, function(data) connectLogic() end);

    Socket:AddNetWorkStateChangeEvent(function(state)
        if state == SocketState.DISCONNECTED then
            Socket:TryConnect(function() connectLogic(); end);
        end
    end)
end

function Update()
    SocketQueue:Update();
end