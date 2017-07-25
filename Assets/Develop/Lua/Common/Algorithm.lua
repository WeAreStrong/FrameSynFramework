module("Algorithm",package.seeall)

function QuickSort(tb, compare, directUseTB)
    local temp = nil;
    if (directUseTB) then
        temp = tb;
    else
        temp = {};
        for _,v in pairs(tb) do
            if (v ~= nil) then
                table.insert(temp, v);
            end
        end
    end
    
    if (#temp > 0) then

        function Sort(left, right)
            if (left >= right) then
                return;
            end
            
            local basic = temp[left];
            local i = left;
            local j = right;
            
            while (i < j) do
                while (i < j) do
                    local res = compare(temp[j], basic);
                    if (res > 0) then
                        break;
                    end
                    j = j - 1;
                end
                
                while (i < j) do
                    local res = compare(basic, temp[i]);
                    if (res > 0) then
                        break;
                    end
                    i = i + 1;
                end
                
                if (i < j) then
                    local t = temp[i];
                    temp[i] = temp[j];
                    temp[j] = t;
                end
            end
            
            temp[left] = temp[i];
            temp[i] = basic;
            
            Sort(left, i - 1);
            Sort(i + 1, right);
        end
        
        Sort(1, #temp);
    end
    
    return temp;
end