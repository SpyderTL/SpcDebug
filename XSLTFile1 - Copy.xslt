<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:ns="http://metalx.org/Mos/6502/Operators" xmlns:prg="http://metalx.org/Program" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:p="http://metalx.org/Platform">
	<xsl:output method="text" indent="yes" />
	<xsl:template match="/">
		<xsl:for-each select="p:platform/p:processor/p:operation">
			<xsl:sort select="@value" data-type="text" order="ascending"/>
			<xsl:text>"</xsl:text>
			<xsl:value-of select="@name"/>
			<xsl:text>",&#x9;&#x9;// </xsl:text>
			<xsl:value-of select="@value"/>
			<xsl:text>&#xd;&#xa;</xsl:text>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>