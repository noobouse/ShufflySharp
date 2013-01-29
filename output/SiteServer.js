require('./mscorlib.js');require('./CommonLibraries.js');require('./CommonShuffleLibrary.js');require('./ShuffleGameLibrary.js');require('./Models.js');require('./RawDeflate.js');
(function() {
	////////////////////////////////////////////////////////////////////////////////
	// SiteServer.SiteClientManager
	var $SiteServer_SiteClientManager = function(siteServerIndex) {
		this.$qManager = null;
		this.$1$SiteServerIndexField = null;
		this.$1$OnUserLoginField = null;
		this.$1$OnGetGameTypesField = null;
		this.$1$OnGetRoomsField = null;
		this.$1$OnGetRoomInfoField = null;
		this.$1$OnCreateRoomField = null;
		this.$1$OnJoinRoomField = null;
		this.set_siteServerIndex(siteServerIndex);
		this.$setup();
	};
	$SiteServer_SiteClientManager.prototype = {
		get_siteServerIndex: function() {
			return this.$1$SiteServerIndexField;
		},
		set_siteServerIndex: function(value) {
			this.$1$SiteServerIndexField = value;
		},
		add_onUserLogin: function(value) {
			this.$1$OnUserLoginField = Function.combine(this.$1$OnUserLoginField, value);
		},
		remove_onUserLogin: function(value) {
			this.$1$OnUserLoginField = Function.remove(this.$1$OnUserLoginField, value);
		},
		add_onGetGameTypes: function(value) {
			this.$1$OnGetGameTypesField = Function.combine(this.$1$OnGetGameTypesField, value);
		},
		remove_onGetGameTypes: function(value) {
			this.$1$OnGetGameTypesField = Function.remove(this.$1$OnGetGameTypesField, value);
		},
		add_onGetRooms: function(value) {
			this.$1$OnGetRoomsField = Function.combine(this.$1$OnGetRoomsField, value);
		},
		remove_onGetRooms: function(value) {
			this.$1$OnGetRoomsField = Function.remove(this.$1$OnGetRoomsField, value);
		},
		add_onGetRoomInfo: function(value) {
			this.$1$OnGetRoomInfoField = Function.combine(this.$1$OnGetRoomInfoField, value);
		},
		remove_onGetRoomInfo: function(value) {
			this.$1$OnGetRoomInfoField = Function.remove(this.$1$OnGetRoomInfoField, value);
		},
		add_onCreateRoom: function(value) {
			this.$1$OnCreateRoomField = Function.combine(this.$1$OnCreateRoomField, value);
		},
		remove_onCreateRoom: function(value) {
			this.$1$OnCreateRoomField = Function.remove(this.$1$OnCreateRoomField, value);
		},
		add_onJoinRoom: function(value) {
			this.$1$OnJoinRoomField = Function.combine(this.$1$OnJoinRoomField, value);
		},
		remove_onJoinRoom: function(value) {
			this.$1$OnJoinRoomField = Function.remove(this.$1$OnJoinRoomField, value);
		},
		$setup: function() {
			this.$qManager = new CommonShuffleLibrary.QueueManager(this.get_siteServerIndex(), new CommonShuffleLibrary.QueueManagerOptions([new CommonShuffleLibrary.QueueWatcher('SiteServer', null), new CommonShuffleLibrary.QueueWatcher(this.get_siteServerIndex(), null)], ['SiteServer', 'GatewayServer', 'Gateway*']));
			this.$qManager.addChannel('Area.Site.Login', Function.mkdel(this, function(user, data) {
				this.$1$OnUserLoginField(user, data);
			}));
			this.$qManager.addChannel('Area.Site.GetGameTypes', Function.mkdel(this, function(user1, data1) {
				this.$1$OnGetGameTypesField(user1);
			}));
			this.$qManager.addChannel('Area.Site.GetRooms', Function.mkdel(this, function(user2, data2) {
				this.$1$OnGetRoomsField(user2, data2);
			}));
			this.$qManager.addChannel('Area.Site.GetRoomInfo', Function.mkdel(this, function(user3, data3) {
				this.$1$OnGetRoomInfoField(user3, data3);
			}));
			this.$qManager.addChannel('Area.Site.CreateRoom', Function.mkdel(this, function(user4, data4) {
				this.$1$OnCreateRoomField(user4, data4);
			}));
			this.$qManager.addChannel('Area.Site.JoinRoom', Function.mkdel(this, function(user5, data5) {
				this.$1$OnJoinRoomField(user5, data5);
			}));
		},
		sendLoginResponse: function(user) {
			this.$qManager.sendMessage(user, user.gateway, 'Area.Site.Login.Response', { successful: true, user: user });
		},
		sendGameTypes: function(user, gameTypes) {
			this.$qManager.sendMessage(user, user.gateway, 'Area.Site.GetGameTypes.Response', gameTypes);
		},
		sendRooms: function(user, response) {
			this.$qManager.sendMessage(user, user.gateway, 'Area.Site.GetRooms.Response', response);
		},
		sendRoomInfo: function(user, response) {
			this.$qManager.sendMessage(user, user.gateway, 'Area.Site.GetRoomInfo.Response', response);
		},
		roomJoined: function(user, roomJoinResponse) {
			this.$qManager.sendMessage(user, user.gateway, 'Area.Site.JoinRoom.Response', roomJoinResponse);
		}
	};
	////////////////////////////////////////////////////////////////////////////////
	// SiteServer.SiteManager
	var $SiteServer_SiteManager = function(siteServerIndex) {
		this.$myDataManager = null;
		this.$myServerManager = null;
		this.$myDataManager = new CommonShuffleLibrary.DataManager();
		this.$myServerManager = new $SiteServer_SiteClientManager(siteServerIndex);
		this.$myServerManager.add_onUserLogin(Function.mkdel(this, this.$onUserLogin));
		this.$myServerManager.add_onGetGameTypes(Function.mkdel(this, this.$onGetGameTypes));
		this.$myServerManager.add_onGetRoomInfo(Function.mkdel(this, this.$onGetRoomInfo));
		this.$myServerManager.add_onGetRooms(Function.mkdel(this, this.$onGetRooms));
		this.$myServerManager.add_onCreateRoom(Function.mkdel(this, this.$onCreateRoom));
		this.$myServerManager.add_onJoinRoom(Function.mkdel(this, this.$onJoinRoom));
	};
	$SiteServer_SiteManager.prototype = {
		$onGetRooms: function(user, data) {
			this.$myDataManager.siteData.room_GetAllByGameType(data.gameType, Function.mkdel(this, function(a) {
				this.$myServerManager.sendRooms(user, { rooms: a });
			}));
		},
		$onGetRoomInfo: function(user, data) {
			this.$myDataManager.siteData.room_GetByRoomName(data.gameType, data.roomName, Function.mkdel(this, function(a) {
				this.$myServerManager.sendRoomInfo(user, { room: a });
			}));
		},
		$onCreateRoom: function(user, data) {
			this.$myDataManager.siteData.room_CreateRoom(data.gameType, data.roomName, user, Function.mkdel(this, function(room) {
				this.$myServerManager.roomJoined(user, { room: room });
				this.$myDataManager.siteData.room_GetAllByGameType(data.gameType, Function.mkdel(this, function(a) {
					this.$myServerManager.sendRooms(user, { rooms: a });
				}));
			}));
		},
		$onJoinRoom: function(user, data) {
			this.$myDataManager.siteData.room_JoinRoom(data.gameType, data.roomName, user, Function.mkdel(this, function(room) {
				this.$myServerManager.roomJoined(user, { room: room });
				for (var $t1 = 0; $t1 < room.players.length; $t1++) {
					var userModel = room.players[$t1];
					this.$myServerManager.sendRoomInfo(userModel, { room: room });
				}
			}));
		},
		$onGetGameTypes: function(user) {
			var $t1 = [];
			$t1.add({ name: 'Blackjack' });
			$t1.add({ name: 'Sevens' });
			var types = $t1;
			this.$myServerManager.sendGameTypes(user, { gameTypes: types });
		},
		$onUserLogin: function(user, data) {
			console.log(user.userName + '  ' + data.hash + '    We did it!');
			this.$myServerManager.sendLoginResponse(user);
		}
	};
	////////////////////////////////////////////////////////////////////////////////
	// SiteServer.SiteServer
	var $SiteServer_SiteServer = function() {
		this.$siteServerIndex = null;
		new global.ArrayUtils();
		this.$siteServerIndex = 'SiteServer' + CommonLibraries.Guid.newGuid();
		process.on('exit', function() {
			console.log('exi SiteServer');
		});
		var siteManager = new $SiteServer_SiteManager(this.$siteServerIndex);
	};
	$SiteServer_SiteServer.main = function() {
		try {
			new $SiteServer_SiteServer();
		}
		catch ($t1) {
			var exc = ss.Exception.wrap($t1);
			console.log('CRITICAL FAILURE: ' + CommonLibraries.ExtensionMethods.goodMessage(exc));
		}
	};
	Type.registerClass(global, 'SiteServer.SiteClientManager', $SiteServer_SiteClientManager, Object);
	Type.registerClass(global, 'SiteServer.SiteManager', $SiteServer_SiteManager, Object);
	Type.registerClass(global, 'SiteServer.SiteServer', $SiteServer_SiteServer, Object);
	$SiteServer_SiteServer.main();
})();
