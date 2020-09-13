<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:ns="http://metalx.org/Mos/6502/Operators" xmlns:prg="http://metalx.org/Program" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:p="http://metalx.org/Platform">
	<xsl:output method="xml" indent="yes" />
	<xsl:template match="/">
		<prg:platform>
			<prg:processor>
				<xsl:for-each select="p:platform/p:processor/p:operation">
					<xsl:sort select="@value" data-type="text" order="ascending"/>
					<prg:operation name="{@name}" value="{@value}"/>
				</xsl:for-each>
			</prg:processor>
		</prg:platform>
	</xsl:template>
</xsl:stylesheet>