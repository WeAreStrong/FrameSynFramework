--------------------------------------------------------------------------------
--      Copyright (c) 2015 - 2016 , 蒙占志(topameng) topameng@gmail.com
--      All rights reserved.
--      Use, modification and distribution are subject to the "MIT License"
--------------------------------------------------------------------------------
if jit then		
	if jit.opt then
		jit.opt.start(3)			
	end
	print("jit", jit.status())
	print(string.format("os: %s, arch: %s", jit.os, jit.arch))
end

if DebugServerIp then  
  require("mobdebug").start(DebugServerIp)
end

local require = require
local string = string
local table = table

function string.split(input, delimiter)
    input = tostring(input)
    delimiter = tostring(delimiter)
    if (delimiter=='') then return false end
    local pos,arr = 0, {}
    -- for each divider found
    for st,sp in function() return string.find(input, delimiter, pos, true) end do
        table.insert(arr, string.sub(input, pos, st - 1))
        pos = sp + 1
    end
    table.insert(arr, string.sub(input, pos))
    return arr
end

function import(moduleName, currentModuleName)
    local currentModuleNameParts
    local moduleFullName = moduleName
    local offset = 1

    while true do
        if string.byte(moduleName, offset) ~= 46 then -- .
            moduleFullName = string.sub(moduleName, offset)
            if currentModuleNameParts and #currentModuleNameParts > 0 then
                moduleFullName = table.concat(currentModuleNameParts, ".") .. "." .. moduleFullName
            end
            break
        end
        offset = offset + 1

        if not currentModuleNameParts then
            if not currentModuleName then
                local n,v = debug.getlocal(3, 1)
                currentModuleName = v
            end

            currentModuleNameParts = string.split(currentModuleName, ".")
        end
        table.remove(currentModuleNameParts, #currentModuleNameParts)
    end

    return require(moduleFullName)
end

--重新require一个lua文件，替代系统文件。
function reimport(name)
    local package = package
    package.loaded[name] = nil
    package.preload[name] = nil
    return require(name)    
end


--逻辑update
function Update(deltaTime, unscaledDeltaTime)
    Time:SetFrameCount();
    Time:SetDeltaTime(deltaTime, unscaledDeltaTime)
    UpdateBeat()
    CoUpdateBeat();
end

function LateUpdate()
	-- LateUpdateBeat()
	-- CoUpdateBeat()
	-- Time:SetFrameCount()
end

--物理update
function FixedUpdate(fixedDeltaTime)
	-- Time:SetFixedDelta(fixedDeltaTime)
	-- FixedUpdateBeat()
end


Mathf		= require "Common.UnityEngine.Mathf"
Vector3 	= require "Common.UnityEngine.Vector3"
Quaternion	= require "Common.UnityEngine.Quaternion"
Vector2		= require "Common.UnityEngine.Vector2"
Vector4		= require "Common.UnityEngine.Vector4"
Color		= require "Common.UnityEngine.Color"
Ray			= require "Common.UnityEngine.Ray"
Bounds		= require "Common.UnityEngine.Bounds"
RaycastHit	= require "Common.UnityEngine.RaycastHit"
Touch		= require "Common.UnityEngine.Touch"
LayerMask	= require "Common.UnityEngine.LayerMask"
Plane		= require "Common.UnityEngine.Plane"
Time		= reimport "Common.UnityEngine.Time"

-- list		= require "Common/list"
-- utf8		= require "Common/utf8"

-- require "Common/event"
-- require "base/typeof"
-- require "slot"
-- require "System.Timer"
-- require "System.coroutine"
require "Common.System.ValueType"
require "Common.System.Reflection.BindingFlags"

--require "misc.strict"
