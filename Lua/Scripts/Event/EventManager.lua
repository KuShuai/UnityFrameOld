EventManager = Class('EventManager')

function EventManager:__init()
	self._eventCallback = {}
end

function EventManager:__delete()
end

function EventManager:Register(id,obj)
	if self._eventCallback[id] == nil then
		self._eventCallback[id]={}
		self._eventCallback[id][obj] = 1
	else
		self._eventCallback[id][obj] = 1
	end
end

function EventManager.Unregister(id,obj)
	if	self._eventCallback[id] ~= nil then
		if	self._eventCallback[id][obj] ~= nil then
			self._eventCallback[id][obj] = nil
		end
	end
end

function EventManager:Notify(id,param)
	if	self._eventCallback[id] ~= nil then
		for	k,v in pairs(self._eventCallback[id]) do
			local obj = k
			obj:OnEvent(id,param)
		end
	end
end