<?xml version="1.0" encoding="UTF-8"?> 
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"> 
<xsl:template match="/"> 
<html> 
<body> 
<h1 align="center">Greatest Chess Players of All Time</h1> 
<h2 align="center">Top 10</h2> 
<table border="3" align="center" > 
<tr> 
	<th>#</th> 
	<th>Name</th> 
	<th>Peek Ranking</th> 
	<th>Born</th> 
	<th>Age</th> 
	<th>Country</th> 
</tr> 
	<xsl:for-each select="chessplayers/s"> 
<tr> 
<td><xsl:value-of select="number"/></td> 
	<td><xsl:value-of select="name"/></td> 
	<td><xsl:value-of select="peekranking"/></td> 
		<td><xsl:value-of select="dob"/></td> 
	<td><xsl:value-of select="age"/></td> 
	<td><xsl:value-of select="country"/></td> 
</tr> 
	</xsl:for-each> 
	</table> 
</body> 
</html> 
</xsl:template> 
</xsl:stylesheet> 
