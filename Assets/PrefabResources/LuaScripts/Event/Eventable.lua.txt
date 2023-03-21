Eventable = Class('Eventable')

function Eventable:__init()
	self._register_events={}
end

function Eventable:__delete()
	for	i = 1,#self._register_events do
		EventManager:Unregister(self._register_events[i],self)
	end
end

function Eventable:_HasEvent(id)
	for	i = 1,#self._register_events do
		if self._register_events[i] == id then
			return i
		end
	end
	return 0
end

function Eventable:RegisterEvent(id)
	if	self:_HasEvent(id) == 0 then
		EventManager.Register(id,self)
		table.insert(self._register_events,id)
	end
end

function Eventable:UnregisterEvent()
	local pos = self:_HasEvent(id)
	if pos ~= 0 then
		EventManager.Unregister(id,self)
		table.remove(self._register_events,pos)
	end
end

function Eventable:OnEvent(id,param)
end