require "Common/ktJson"
require "Common/LuaExtension"
SocketQueue = require('SocketQueue')
require "Demo1"
require "Demo2"

local mTcpPrefix = 'fifa-dev.mbgadev.cn';
--local mTcpPrefix = '172.21.175.94';
local mTcpPort = 3050;

ROOM_ID = 99;
ROOM_SIZE = 1;

FrameType =
{
    Fill = 0,
    Key = 1,
};

function Main()
    local socket = require("Socket");
    Socket = socket.new();

    Socket:AddNetWorkStateChangeEvent(function(state)
        if state == SocketState.DISCONNECTED then
            Socket:TryConnect(function() connectLogic(); end);
        end
    end)
end

function Update()
    SocketQueue:Update();
end

function SetDemo1(_uid)
    local user = 
    {
        uid = _uid,
        sid = 1
    }
    function connectLogic()
        Socket:Connect(user,
            function()
                Socket:Request1('connector.entryHandler.enter', user,
                    function() Demo1.Ready(); end);
            end);
    end
    Socket:InitClient(mTcpPrefix, mTcpPort, function(data) connectLogic() end);
end

function SetDemo2(_uid)
    local user = 
    {
        uid = _uid,
        sid = 1
    }
    function connectLogic()
        Socket:Connect(user,
            function()
                Socket:Request1('connector.entryHandler.enter', user,
                    function() Demo2.Ready(); end);
            end);
    end
    Socket:InitClient(mTcpPrefix, mTcpPort, function(data) connectLogic() end);
end