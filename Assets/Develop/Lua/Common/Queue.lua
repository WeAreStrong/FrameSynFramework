local Queue = class("Queue")

function Queue:ctor(capacity)
    self.mCapacity = capacity or 9999999
    self.mQueue = {}
    self.mSize = 0
    self.mHead = -1
    self.mRear = -1
end

function Queue:Push(element)
    if self.mSize == 0 then
        self.mHead = 0
        self.mRear = 1
        self.mSize = 1
        self.mQueue[self.mRear] = element
     else
         local temp = (self.mRear + 1) % self.mCapacity
         if temp == self.mHead then
             colorlog("Error: capacity is full.","#FF000000")
             return 
         else
             self.mRear = temp
         end
         self.mQueue[self.mRear] = element
         self.mSize = self.mSize + 1
     end
end
--如果超过上限就强行挤进去,把最前面的人挤出去 
function Queue:HardPush(element)
    while self.mSize >= self.mCapacity do
        self:pop()
    end
    self:Push(element)
end

function Queue:Pop()
    if self:IsEmpty() then
        return
    end
    self.mSize = self.mSize - 1
    self.mHead = (self.mHead + 1) % self.mCapacity
    local value = self.mQueue[self.mHead]
    return value
end

function Queue:Clear()
    self.mQueue = nil
    self.mQueue = {}
    self.mSize = 0
    self.mHead = -1
    self.mRear = -1
end
function Queue:IsEmpty()
    if self:Size() == 0 then
        return true
    end
    return false
end

function Queue:Size()
    return self.mSize
end

function Queue:printElement()
    local h = self.mHead
    local r = self.mRear
    local str = nil
    local first_flag = true
    while h ~= r do
        if first_flag == true then
            str = "{"..self.mQueue[h]
            h = (h + 1) % self.mCapacity
            first_flag = false
        else
            str = str..","..self.mQueue[h]
            h = (h + 1) % self.mCapacity
        end
    end
    str = str..","..self.mQueue[r].."}"
    log(str)
end

return Queue
