local ui_config = {}

function AddUIConfig(id,res_path,layer,full_screen)
	ui_config[id] = {
		['name']=res_path,
		['layer'] = layer,
		['fullscreen'] = full_screen
	}
end

function GetUIConfig(id)

	Debug("AAAAA1111"..id..'	'..ui_config[id].name)
	
		--for	k,v in pairs(ui_config) do
			--Debug("uiconfig" .. k..'	'..v.name)
		--end
	return ui_config[id]
end

AddUIConfig(
	UIPanelEnum.UIPanel1,
	'UIPanel1',
	CS.UILayer.Top,
	true
)

AddUIConfig(
	UIPanelEnum.UIPanel2,
	'UIPanel2',
	CS.UILayer.Top,
	true
)

AddUIConfig(
	UIPanelEnum.UIPanel3,
	'UIPanel3',
	CS.UILayer.Top,
	true
)