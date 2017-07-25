local Socket = class('Socket')

SocketState = 
{
    CLOSE = 0 ,
    CONNECTING = 1 ,
    CONNECTED = 2 ,
    DISCONNECTED = 3 ,
    TIMEOUT = 4 ,
    ERROR = 5
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

function Socket:Request(route,msg)
    local m = json.encode(msg)
    return ClientSocket.Request(route,m)
end

function Socket:OnPushEvent(route,func)
    return ClientSocket.On(route,func)
end

function Socket:TryConnect(func)
    return ClientSocket.TryReconnect(func)
end

function Socket:AddNetWorkStateChangeEvent(func)
    return ClientSocket.AddNetWorkStateChangeEvent(func)
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