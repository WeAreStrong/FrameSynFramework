require "Common/ktJson"
require "Common/LuaExtension"
require "TestMove"
SocketQueue = require('SocketQueue')

local mTcpPrefix = 'fifa-dev.mbgadev.cn';
local mTcpPort = 3050;

function Main()
	local socket = require("Socket");
	Socket = socket.new();
    local user = 
    {
        uid = "58f7035db4613d0b01e93770",
        sid = 1
    }

    function connectLogic()
        Socket:Connect(user, function() Socket:Request('connector.entryHandler.enter', user) end);
        TestMove.Init();
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