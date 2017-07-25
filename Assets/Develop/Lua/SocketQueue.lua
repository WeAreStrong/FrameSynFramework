
local Queue = require('Common/Queue')

local SocketQueue = class('SocketQueue')

function SocketQueue:ctor()
	self.eventQueue = Queue.new()
end

function SocketQueue:Push(func)
	self.eventQueue:Push(func)
end

function SocketQueue:Update()
	while true do
		local func = self.eventQueue:Pop()
		if func then
			func()
		else
			break
		end
	end
end

return SocketQueue.new()