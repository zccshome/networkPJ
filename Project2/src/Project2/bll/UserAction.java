package Project2.bll;

import java.sql.*;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Map;
import java.util.List;
import java.util.ArrayList;

public class UserAction
{
	//Here defines how to connector the database.
	private static final String connectionString = "jdbc:mysql://localhost:3306/mblog";
	//username for mysql, 'root' by default.
	private static final String dbUsername = "root";
	//user password for mysql, change to yours.
	private static final String dbPassword = "949351";
	
	private static final String LoginSQL = "SELECT * FROM user where username = ? and password = ?";
	private static final String RegSQL = "INSERT INTO user(username,password,nickname,comefrom,regtime) values(?,?,?,?,now())";
	private static final String GetUserSQL = "SELECT * FROM user where iduser = ?";
	//private static final String getHotSQL = "SELECT * FROM relation ORDER BY target";
	//private static final String getFriend = "SELECT user.iduser, user.nickname, relation.target FROM user, relation where user.iduser = relation.source";
	//private static final String getFan = "SELECT user.iduser, relation.source FROM user, relation where user.iduser = relation.target order by iduser";
	private static final String getFan2 = "SELECT * FROM relation order by target";
	private static final String getNewSQL = "SELECT * FROM user ORDER BY regtime DESC";
	private static final String getFanById = "SELECT source FROM relation where target = ?";
	private static final String getFriendById = "SELECT target FROM relation where source = ?";
	private static final String getWeiboById = "SELECT * FROM mblogs where idpublisher = ? ORDER BY time DESC";
	private static final String getOriginalWeiboById = "SELECT * FROM mblogs where idmblogs = ?";
	private static final String getzhuanfaById = "SELECT * FROM mblogs where idoriginalmblog = ?";
	private static final String getpinglunNumber = "SELECT * FROM comment where idtargetmblog = ? ORDER BY time DESC";
	private static final String getWholeWeibo = "SELECT * FROM mblogs ORDER BY time DESC";
	private static final String getRelativePerson = "SELECT * FROM user";
	private static final String getRelativeWeibo = "SELECT * FROM mblogs";
	private static final String insertWeibo = "INSERT INTO mblogs(txt,idpublisher,time) values(?,?,now())";
	private static final String insertPinglun = "INSERT INTO comment(txt,idpublisher,idtargetmblog,time) values(?,?,?,now())";
	private static final String addFriend = "INSERT INTO relation(source,target) values (?,?)";
	private static final String deleteFriend = "DELETE FROM relation where source = ? and target = ?";
	private static final String insertWeibo2 = "INSERT INTO mblogs(txt,idpublisher,idoriginalmblog,time) values(?,?,?,now())";
	private static final String checkReg = "SELECT * FROM user where username = ?";
	static
	{
		try {
			//class name for mysql driver
			Class.forName("com.mysql.jdbc.Driver");
		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	//用户登录时调用的方法
	public static User Login(String username, String password) throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(LoginSQL);
			ps.setString(1, username);
			ps.setString(2, password);
			ResultSet newid = ps.executeQuery();
			if (newid.next())
			{
				int id = Integer.parseInt(newid.getString("iduser"));
				User user = new User();
				user.setId(id);
				user.setNickname(newid.getString("nickname"));
				user.setUsername(newid.getString("username"));
				user.setComefrom(newid.getString("comefrom"));
				user.setRegTime(Timestamp.valueOf(newid.getString("regtime")));
				return user;
			}
			else
			{
				return null;
			}
		}
		catch (SQLException e)
		{
			return null;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		
	}
	//用户注册时调用的方法，2为邮箱重复，1为数据库失败，0为成功
	public static int Reg(User user) throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(checkReg);
			ps.setString(1, user.getUsername());
			ResultSet newid5 = ps.executeQuery();
			if(newid5.next())
				return 2;
			ps = c.prepareStatement(RegSQL,Statement.RETURN_GENERATED_KEYS);
			ps.setString(1, user.getUsername());
			ps.setString(2, user.getPassword());
			ps.setString(3, user.getNickname());
			ps.setString(4, user.getComefrom());
			ps.executeUpdate();
			ResultSet newid = ps.getGeneratedKeys();
			if (newid.next())
			{
				user.setId(newid.getInt(1));
				PreparedStatement ps2 = c.prepareStatement(GetUserSQL);
				ps2.setInt(1, user.getId());
				ResultSet rs2 = ps2.executeQuery();
				if (rs2.next())
				{
					user.setRegTime(rs2.getTimestamp("regtime"));
				}
				return 0;
			}
			else
			{
				return 1;
			}
		}
		catch (SQLException e)
		{
			return 1;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
	}
	//得到热门用户列表，自己重写了排序方法，如果数据库连接失败，返回一个空列表
	public static ArrayList<String[]> getHot() throws SQLException
	{
		Map<Integer,Integer> fanList = new HashMap<Integer,Integer>();
		ArrayList<String[]> hotList = new ArrayList<String[]>();
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(getFan2);
			ResultSet newid = ps.executeQuery();
			while (newid.next())
			{
				int iduser = Integer.parseInt(newid.getString("target"));
				if(fanList.containsKey(iduser))
					fanList.put(iduser, fanList.get(iduser)+1);
				else
					fanList.put(iduser, 1);
			}
			List<Map.Entry<Integer, Integer>> list_Data = new ArrayList<Map.Entry<Integer, Integer>>(fanList.entrySet());
		    Collections.sort(list_Data, new Comparator<Map.Entry<Integer, Integer>>()
		    {  
		    	public int compare(Map.Entry<Integer, Integer> o1, Map.Entry<Integer, Integer> o2)
		        {
		        	if(o2.getValue()!=null&&o1.getValue()!=null&&o2.getValue().compareTo(o1.getValue())>0){
		        		return 1;
		            }
		        	else
		        	{
		        		return -1;
		            }
		        }
		    });
			for(int i = 0; i < Math.min(6, fanList.size()); i++)
			{
				String[] hot = new String[3];
				//System.out.println(list_Data.get(i).getKey());
				PreparedStatement ps2 = c.prepareStatement(GetUserSQL);
				ps2.setInt(1, list_Data.get(i).getKey());
				ResultSet rs2 = ps2.executeQuery();
				if (rs2.next())
				{
					hot[0] = rs2.getString("nickname");
					hot[1] = ""+list_Data.get(i).getValue();
					hot[2] = ""+list_Data.get(i).getKey();
					hotList.add(hot);
				}
			}
		}
		catch (SQLException e)
		{
			return new ArrayList<String[]>();
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return hotList;
	}
	//得到新注册用户列表，如果数据库连接失败，返回一个空列表
	public static ArrayList<String[]> getNew() throws SQLException
	{
		ArrayList<String[]> hotList = new ArrayList<String[]>();
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(getNewSQL);
			ResultSet newid = ps.executeQuery();
			int i = 0;
			while (newid.next() && i < 8)
			{
				String[] hot = new String[2];
				hot[0] = newid.getString("nickname");
				hot[1] = newid.getString("iduser");
				hotList.add(hot);
				i++;
			}
		}
		catch (SQLException e)
		{
			return new ArrayList<String[]>();
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return hotList;
	}
	//得到可能感兴趣的人名单，就是好友＋粉丝，（可能有重复）。如果数据库连接失败，返回一个空列表
	public static ArrayList<String[]> getFriend(int number) throws SQLException
	{
		ArrayList<String[]> hotList = new ArrayList<String[]>();
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(getFanById);
			ps.setInt(1, number);
			ResultSet newid = ps.executeQuery();
			int i = 0;
			while (newid.next() && i < 8)
			{
				int target = newid.getInt("source");
				ps = c.prepareStatement(GetUserSQL);
				ps.setInt(1, target);
				ResultSet newid2 = ps.executeQuery();
				if(newid2.next())
				{
					String[] hot = new String[2];
					hot[0] = newid2.getString("nickname");
					hot[1] = newid2.getString("iduser");
					hotList.add(hot);
				}
				i++;
			}
			if(hotList.size() < 8)
			{
			ps = c.prepareStatement(getFriendById);
			ps.setInt(1, number);
			ResultSet newid3 = ps.executeQuery();
			i = hotList.size();
			while (newid3.next() && i < 8)
			{
				int target = newid3.getInt("target");
				ps = c.prepareStatement(GetUserSQL);
				ps.setInt(1, target);
				ResultSet newid2 = ps.executeQuery();
				if(newid2.next())
				{
					String[] hot = new String[2];
					hot[0] = newid2.getString("nickname");
					hot[1] = newid2.getString("iduser");
					hotList.add(hot);
				}
				i++;
			}
			}
		}
		catch (SQLException e)
		{
			return new ArrayList<String[]>();
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return hotList;
	}
	//得到好友列表，只取前5个。如果数据库连接失败，返回一个空列表
	public static ArrayList<String[]> getFriendPerson(int number) throws SQLException
	{
		ArrayList<String[]> friendList = new ArrayList<String[]>();
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(getFriendById);
			ps.setInt(1, number);
			ResultSet newid = ps.executeQuery();
			int i = 0;
			while (newid.next() && i < 5)
			{
				int target = newid.getInt("target");
				ps = c.prepareStatement(GetUserSQL);
				ps.setInt(1, target);
				ResultSet newid2 = ps.executeQuery();
				String[] friend = new String[5];
				if(newid2.next())
				{
					friend[1] = newid2.getString("nickname");
					friend[0] = newid2.getString("iduser");
					friend[2] = newid2.getString("comefrom");
					friend[3] = ""+UserAction.getFanNumber(newid2.getInt("iduser"));
				}
				friend[4] = "TA很懒，什么都没有留下。";
				ps = c.prepareStatement(getWeiboById);
				ps.setInt(1, target);
				ResultSet newid3 = ps.executeQuery();
				if(newid3.next())
				{
					friend[4] = newid3.getString("txt")+newid3.getString("time");
				}
				friendList.add(friend);
				i++;
			}
		}
		catch (SQLException e)
		{
			return new ArrayList<String[]>();
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return friendList;
	}
	//得到粉丝列表，只取前5个。如果数据库连接失败，返回一个空列表
	public static ArrayList<String[]> getFanPerson(int number) throws SQLException
	{
		ArrayList<String[]> friendList = new ArrayList<String[]>();
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(getFanById);
			ps.setInt(1, number);
			ResultSet newid = ps.executeQuery();
			int i = 0;
			while (newid.next() && i < 5)
			{
				int target = newid.getInt("source");
				ps = c.prepareStatement(GetUserSQL);
				ps.setInt(1, target);
				ResultSet newid2 = ps.executeQuery();
				String[] friend = new String[5];
				if(newid2.next())
				{
					friend[1] = newid2.getString("nickname");
					friend[0] = newid2.getString("iduser");
					friend[2] = newid2.getString("comefrom");
					friend[3] = ""+UserAction.getFanNumber(newid2.getInt("iduser"));
				}
				friend[4] = "TA很懒，什么都没有留下。";
				ps = c.prepareStatement(getWeiboById);
				ps.setInt(1, target);
				ResultSet newid3 = ps.executeQuery();
				if(newid3.next())
				{
					friend[4] = newid3.getString("txt")+newid3.getString("time");
				}
				friendList.add(friend);
				i++;
			}
		}
		catch (SQLException e)
		{
			return new ArrayList<String[]>();
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return friendList;
	}
	//得到粉丝数量
	public static int getFanNumber(int number) throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		int i = 0;
		try
		{
			ps = c.prepareStatement(getFanById);
			ps.setInt(1, number);
			ResultSet newid = ps.executeQuery();
			while (newid.next())
			{
				i++;
			}
		}
		catch (SQLException e)
		{
			return 0;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return i;
	}
	//判断是不是好友
	public static boolean isFriend(int source, int target) throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		boolean ret = false;
		try
		{
			ps = c.prepareStatement(getFriendById);
			ps.setInt(1, source);
			ResultSet newid = ps.executeQuery();
			while (newid.next())
			{
				int target2 = newid.getInt("target");
				if(target==target2)
					ret = true;
			}
		}
		catch (SQLException e)
		{
			return false;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return ret;
	}
	//得到好友数量
	public static int getFriendNumber(int number) throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		int i = 0;
		try
		{
			ps = c.prepareStatement(getFriendById);
			ps.setInt(1, number);
			ResultSet newid = ps.executeQuery();
			while (newid.next())
			{
				i++;
			}
		}
		catch (SQLException e)
		{
			return 0;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return i;
	}
	//得到微博数量
	public static int getWeiboNumber(int number) throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		int i = 0;
		try
		{
			ps = c.prepareStatement(getWeiboById);
			ps.setInt(1, number);
			ResultSet newid = ps.executeQuery();
			while (newid.next())
			{
				i++;
			}
		}
		catch (SQLException e)
		{
			return 0;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return i;
	}
	//得到眸ID用户的nickname
	public static String getNickNameById(int id) throws SQLException
	{
		String nickname = "";
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		ps = c.prepareStatement(GetUserSQL);
		ps.setInt(1, id);
		ResultSet newid = ps.executeQuery();
		if(newid.next())
		{
			nickname = newid.getString("nickname");
		}
		return nickname;
	}
	//得到眸ID用户的comefrom
	public static String getcomefromById(int id) throws SQLException
	{
		String nickname = "";
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		ps = c.prepareStatement(GetUserSQL);
		ps.setInt(1, id);
		ResultSet newid = ps.executeQuery();
		if(newid.next())
		{
			nickname = newid.getString("comefrom");
		}
		return nickname;
	}
	//得到眸用户的微博，只取100个
	public static ArrayList<String[]> getWeibo(int number) throws SQLException
	{
		ArrayList<String[]> weiboList = new ArrayList<String[]>();
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(getWeiboById);
			ps.setInt(1, number);
			ResultSet newid = ps.executeQuery();
			int i = 0;
			while (newid.next()&&i<100)
			{
				String[] weibos = new String[10];
				weibos[0] = newid.getString("txt");
				weibos[1] = newid.getString("time");
				weibos[2] = UserAction.getNickNameById(newid.getInt("idpublisher"));
				weibos[3] = newid.getString("idpublisher");
				weibos[4] = "";
				weibos[5] = "";
				weibos[6] = ""+UserAction.getzhuanfaNumber(newid.getInt("idmblogs"));
				if(newid.getInt("idoriginalmblog") != 0)
				{
					ps = c.prepareStatement(getOriginalWeiboById);
					ps.setInt(1, newid.getInt("idoriginalmblog"));
					ResultSet newid2 = ps.executeQuery();
					if(newid2.next())
					{
						weibos[4] = newid2.getString("txt");
						weibos[5] = UserAction.getNickNameById(newid2.getInt("idpublisher"));
						weibos[8] = ""+newid2.getInt("idpublisher");
						weibos[9] = ""+newid2.getInt("idmblogs");
					}
				}
				weibos[7] = ""+newid.getInt("idmblogs");
				weiboList.add(weibos);
				i++;
			}
		}
		catch (SQLException e)
		{
			return new ArrayList<String[]>();
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return weiboList;
	}
	//得到全站微博，只取100个
	public static ArrayList<String[]> getWholeWeibo() throws SQLException
	{
		ArrayList<String[]> weiboList = new ArrayList<String[]>();
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(getWholeWeibo);
			ResultSet newid = ps.executeQuery();
			int i = 0;
			while (newid.next()&&i<100)
			{
				String[] weibos = new String[10];
				weibos[0] = newid.getString("txt");
				weibos[1] = newid.getString("time");
				weibos[2] = UserAction.getNickNameById(newid.getInt("idpublisher"));
				weibos[3] = newid.getString("idpublisher");
				weibos[4] = "";
				weibos[5] = "";
				weibos[6] = ""+UserAction.getzhuanfaNumber(newid.getInt("idmblogs"));
				if(newid.getInt("idoriginalmblog") != 0)
				{
					ps = c.prepareStatement(getOriginalWeiboById);
					ps.setInt(1, newid.getInt("idoriginalmblog"));
					ResultSet newid2 = ps.executeQuery();
					if(newid2.next())
					{
						weibos[4] = newid2.getString("txt");
						weibos[5] = UserAction.getNickNameById(newid2.getInt("idpublisher"));
						weibos[8] = ""+newid2.getInt("idpublisher");
						weibos[9] = ""+newid2.getInt("idmblogs");
					}
				}
				weibos[7] = ""+newid.getInt("idmblogs");
				weiboList.add(weibos);
				i++;
			}
		}
		catch (SQLException e)
		{
			return new ArrayList<String[]>();
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return weiboList;
	}
	//得到关注用户的微博，只取100个
	public static ArrayList<String[]> getGuanzhuWeibo(int number) throws SQLException
	{
		ArrayList<String[]> weiboList = new ArrayList<String[]>();
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(getFriendById);
			ps.setInt(1, number);
			ResultSet newid = ps.executeQuery();
			ArrayList<Integer> friendList = new ArrayList<Integer>();
			while (newid.next())
			{
				friendList.add(newid.getInt("target"));
			}
			for(int i = 0; i < friendList.size(); i++)
			{
				ArrayList<String[]> tempList = UserAction.getWeibo(friendList.get(i));
				weiboList.addAll(tempList);
			}
			Collections.sort(weiboList, new Comparator<String[]>()
			{  
				public int compare(String[] o1, String[] o2)
				{
				     if(o1[1].compareTo(o2[1])<0)
				     {
				        return 1;
				     }
				     else
				     {
				     	return -1;
				     }
				}
			});
		}
		catch (SQLException e)
		{
			return new ArrayList<String[]>();
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		if(weiboList.size()<100)
			return weiboList;
		else
		{
			ArrayList<String[]> weiboList2 = new ArrayList<String[]>();
			for(int i = 0; i < 100; i++)
				weiboList2.add(weiboList.get(i));
			return weiboList2;
		}
	}
	//得到关注的数量
	public static int getzhuanfaNumber(int number) throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		int i = 0;
		try
		{
			ps = c.prepareStatement(getzhuanfaById);
			ps.setInt(1, number);
			ResultSet newid = ps.executeQuery();
			while (newid.next())
			{
				i++;
			}
		}
		catch (SQLException e)
		{
			return 0;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return i;
	}
	//得到某微博的评论数
	public static int getpinglunNumber(int mblog) throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		int i = 0;
		try
		{
			ps = c.prepareStatement(getpinglunNumber);
			ps.setInt(1, mblog);
			ResultSet newid = ps.executeQuery();
			while (newid.next())
			{
				i++;
			}
		}
		catch (SQLException e)
		{
			return 0;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return i;
	}
	//得到某微博的评论
	public static ArrayList<String[]> getpinglun(int mblog) throws SQLException
	{
		ArrayList<String[]> commentArray = new ArrayList<String[]>();
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		int i = 0;
		try
		{
			ps = c.prepareStatement(getpinglunNumber);
			ps.setInt(1, mblog);
			ResultSet newid = ps.executeQuery();
			while (newid.next() && i < 100)
			{
				String[] commenter = new String[5];
				int id = newid.getInt("idpublisher");
				commenter[0] = ""+id;
				commenter[1] = newid.getString("txt");
				commenter[2] = newid.getString("time");
				commenter[3] = UserAction.getNickNameById(id);
				commenter[4] = newid.getString("idcomment");
				commentArray.add(commenter);
				i++;
			}
		}
		catch (SQLException e)
		{
			return new ArrayList<String[]>();
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
		return commentArray;
	}
	//得到搜索的用户或者人
	public static ArrayList<String[]> getRelativePerson(String tempString)throws SQLException
	{
		ArrayList<String[]> personList = new ArrayList<String[]>();
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(getRelativePerson);
			ResultSet newid = ps.executeQuery();
			int i = 0;
			while (newid.next() && i < 6)
			{
				String nickname = newid.getString("nickname");
				if(nickname.contains(tempString))
				{
					String[] miaomiao = new String[5];
					int id = newid.getInt("iduser");
					miaomiao[0] = newid.getString("iduser");
					miaomiao[1] = newid.getString("nickname");
					miaomiao[2] = newid.getString("comefrom");
					miaomiao[3] = ""+UserAction.getFanNumber(id);
					ArrayList<String[]> a = UserAction.getWeibo(id);
					miaomiao[4] = "";
					if(a.size()>0)
						miaomiao[4] = a.get(0)[0]+"("+a.get(0)[1]+")";
					else
						miaomiao[4] = "TA很懒，什么都没有留下。";
					personList.add(miaomiao);
					i++;
				}
			}
			ps = c.prepareStatement(getRelativeWeibo);
			ResultSet newid2 = ps.executeQuery();
			int j = 0;
			while (newid2.next() && j < 6)
			{
				String txt = newid2.getString("txt");
				if(txt.contains(tempString))
				{
					String[] miaomiao = new String[5];
					miaomiao[4] = txt+"("+newid2.getString("time")+")";
					int id = newid2.getInt("idpublisher");
					miaomiao[0] = newid2.getString("idpublisher");
					miaomiao[1] = UserAction.getNickNameById(id);
					miaomiao[2] = UserAction.getcomefromById(id);
					miaomiao[3] = ""+UserAction.getFanNumber(id);
					personList.add(miaomiao);
					j++;
				}
			}
			if(personList.size()<6)
				return personList;
			else
			{
				ArrayList<String[]> personList2 = new ArrayList<String[]>();
				while(personList2.size() < 6)
				{
					String[] temp1 = personList.remove(0);
					String[] temp2 = personList.remove(personList.size()-1);
					personList2.add(temp1);
					personList2.add(temp2);
				}
				return personList2;
			}
		}
		catch (SQLException e)
		{
			return new ArrayList<String[]>();
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
	}
	//发布微博
	public static boolean insertWeibo(String txt, int idpublisher)throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(insertWeibo,Statement.RETURN_GENERATED_KEYS);
			ps.setString(1, txt);
			ps.setInt(2, idpublisher);
			ps.executeUpdate();
			return true;
		}
		catch (SQLException e)
		{
			return false;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
	}
	//转发微博
	public static boolean insertWeibo2(String txt, int idpublisher, int idoriginalmblog)throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(insertWeibo2,Statement.RETURN_GENERATED_KEYS);
			ps.setString(1, txt);
			ps.setInt(2, idpublisher);
			ps.setInt(3, idoriginalmblog);
			ps.executeUpdate();
			return true;
		}
		catch (SQLException e)
		{
			return false;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
	}
	//插入评论
	public static boolean insertPinglun(String txt, int idpublisher, int idtargetmblog)throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(insertPinglun,Statement.RETURN_GENERATED_KEYS);
			ps.setString(1, txt);
			ps.setInt(2, idpublisher);
			ps.setInt(3, idtargetmblog);
			ps.executeUpdate();
			return true;
		}
		catch (SQLException e)
		{
			return false;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
	}
	//加好友
	public static boolean addFriend(int source, int target)throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(addFriend,Statement.RETURN_GENERATED_KEYS);
			ps.setInt(1, source);
			ps.setInt(2,target);
			ps.executeUpdate();
			return true;
		}
		catch (SQLException e)
		{
			return false;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
	}
	//删好友
	public static boolean deleteFriend(int source, int target)throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(deleteFriend,Statement.RETURN_GENERATED_KEYS);
			ps.setInt(1, source);
			ps.setInt(2,target);
			ps.executeUpdate();
			return true;
		}
		catch (SQLException e)
		{
			return false;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
	}
}
