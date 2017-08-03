
require("Common/ktjson")
require("Common/LuaExtension")

local Socket = class('Socket')

--socket状态
SocketState = 
{
    CLOSE = 0 ,
    CONNECTING = 1 ,
    CONNECTED = 2 ,
    DISCONNECTED = 3 ,
    TIMEOUT = 4 ,
    ERROR = 5 ,
    KICKED = 6
}

function Socket:ctor()
    self.m_kName = 'Socket'
    -- self:Init()
end

function Socket:InitClient(host,port,cb)
    return ClientSocket.InitClient(host,port,cb)
end

function Socket:Connect(user,func)
    local function f(jsonmsg)
        local data = json.decode(jsonmsg)
        func(data)
    end
    return ClientSocket.Connect(user,self:GetKeys(user),f)
end

function Socket:Notify(route,msg)
    local m = json.encode(msg)
    return ClientSocket.Notify(route,m)
end

function Socket:Request(route,msg)
    local function f(msg)
        -- SocketQueue:Push(function() 
        --     CommandManager:SendCommand(route,json.decode(msg))
        -- end)
    end
    local m = json.encode(msg)
    return ClientSocket.Request(route,m,f)
end

-- 直接带回调的请求，尽量不要用
function Socket:Request1(route,msg,func)
    local m = json.encode(msg)
    return ClientSocket.Request(route,m,func)
end

function Socket:OnPushEvent(route,func)
    return ClientSocket.On(route,func)
end

function Socket:TryReconnect(func)
    return ClientSocket.TryReconnect(func)
end

function Socket:AddNetWorkStateChangeEvent(func)
    return ClientSocket.AddNetWorkStateChangeEvent(func)
end

function Socket:SendDelayRequest()
    return ClientSocket.SendDelayRequest()
end

function Socket:GetKeys(table)
    local str = ''
    if not table then return str end
	for k,_ in pairs(table) do
		str = str..k..'|'
	end
	return str
end

return Socket