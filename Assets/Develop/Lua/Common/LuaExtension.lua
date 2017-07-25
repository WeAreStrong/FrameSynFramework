function class(classname,super)
	local superType = type(super)
	local cls
	if super then
		cls = {}
		setmetatable(cls, {__index = super})
		cls.super = super
	else
		cls = {ctor = function() end}
	end

	cls.__cname = classname
	cls.__index = cls

	function cls.new(...)
		local instance = setmetatable({}, cls)
		instance.super = cls.super
		instance:ctor(...)
		return instance
	end
	return cls	
end

function IsTableEmpty( tableToTest )
    if(tableToTest==nil)then
        return true;
    end
    return (next(tableToTest) == nil);
end

function TableContainValue(tb, value)
    for _, v in ipairs(tb) do
        if (v == value) then
            return true;
        end
    end
    
    return false;
end


-- ============ Begin Log Functions ===================
--输出日志--
function log(str)
	-- local res = ""
    -- for k,v in ipairs({...}) do
    --     res = res .. tostring(v) .. '\t'
    -- end
    -- res = res .. debug.traceback("")
    LuaHelper.ColorLog(str,"#FFFFFFFF");
end 

function colorlog(str,color)
	LuaHelper.ColorLog(str,color);
end

--错误日志--
function logError(str) 
	LuaHelper.LogError(str);
end

--警告日志--
function logWarn(str) 
	LuaHelper.LogWarning(str);
end

function GetLocalizedString(str)
    return str;
end

-- ============    End Log Functions    =================

-- ============ Begin Utility Functions =================

function GetLocalizedString(str)
    return Util.LocalizeString(str);
end

-- ============  End Utility Functions  =================

-- 参数:待分割的字符串,分割字符
-- 返回:子串表.(含有空串)
function StringSplit(str, split_char)
    local sub_str_tab = {};
    while (true) do
        local pos = string.find(str, split_char);
        if (not pos) then
            sub_str_tab[#sub_str_tab + 1] = str;
            break;
        end
        local sub_str = string.sub(str, 1, pos - 1);
        sub_str_tab[#sub_str_tab + 1] = sub_str;
        str = string.sub(str, pos + 1, #str);
    end
 
    return sub_str_tab;
end

--lua table 深度拷贝.共用同一个metatable
function DeepCopy(obj)
    local tempTable = {};

    local function copy(obj)
        if type(obj) ~= "table" then
            return obj
        elseif tempTable[obj] then
            return tempTable[obj]
        end
        local newTable = {};
        tempTable[obj] = newTable;
        for index, value in pairs(obj) do
            newTable[copy(index)] = copy(value);
        end
        return setmetatable(newTable, getmetatable(obj))
    end

    return copy(obj);
end
--lua table 深度拷贝包括metatable也拷贝一份
function DeepCopyIncludeMetatable(obj)
    local tempTable = {};

    local function copy(obj)
        if type(obj) ~= "table" then
            return obj
        elseif tempTable[obj] then
            return tempTable[obj]
        end
        local newTable = {};
        tempTable[obj] = newTable;
        for index, value in pairs(obj) do
            newTable[copy(index)] = copy(value);
        end
        
        --return setmetatable(newTable, getmetatable(obj))
        return setmetatable(newTable, copy(getmetatable(obj)))
    end

    return copy(obj);
end